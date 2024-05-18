using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HMY.Helpers.Collections;
using HMY.Infrastructure.AsyncResponse;
using Mapster;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Trackrypto.Helpers;
using Trackrypto.Model;
using Trackrypto.Model.Entities;
using Trackrypto.Repositories;
using Trackrypto.Utils;
using Trackrypto.View.Dialogs;
using Trackrypto.ViewModel.EntityViewModel;
using Trackrypto.ViewModel.Messenger;
using Trackrypto.ViewModel.Navigation;
using Trackrypto.ViewModel.ViewViewModel.Filters;

namespace Trackrypto.ViewModel.ViewViewModel
{
    public class TransactionListViewModel : ViewModelBase, IPageViewModel
    {
        public RangeObservableCollection<TransaccionViewModel> Transacciones { get; set; }
        private TransaccionViewModel selectedTransaccion;
        public TransaccionViewModel SelectedTransaccion
        {
            get => selectedTransaccion;
            set
            {
                selectedTransaccion = value;
                RaisePropertyChanged();
            }
        }


        #region constructor
        public TransactionListViewModel()
        {
            Transacciones = new RangeObservableCollection<TransaccionViewModel>();

            InitializeFilters();

            WireCommands();
            RegisterMessenger();
            ConfigurePagination();

            Update();
        }
        #endregion

        #region filters
        private bool alertaFilter;
        public bool AlertaFilter 
        {
            get => alertaFilter;
            set
            {
                alertaFilter = value;
                Update();
            }
        }
        public FilterViewModel TipoFilterViewModel { get; private set; }
        public FilterViewModel SubtipoFilterViewModel { get; private set; }
        public FilterViewModel ExchangeFilterViewModel { get; private set; }
        public FilterViewModel SimboloFilterViewModel { get; private set; }

        private void InitializeFilters()
        {
            TipoFilterViewModel = new FilterViewModel("TIPO");
            SubtipoFilterViewModel = new FilterViewModel("SUBTIPO");
            ExchangeFilterViewModel = new FilterViewModel("EXCHANGE");
            SimboloFilterViewModel = new FilterViewModel("SIMBOLO");
            
            ReplaceFilters();
        }

        private void ReplaceFilters()
        {
            alertaFilter = false;
            TipoFilterViewModel.ReplaceFilters(TransaccionesRepository.GetTipos());
            SubtipoFilterViewModel.ReplaceFilters(TransaccionesRepository.GetSubtipos());
            ExchangeFilterViewModel.ReplaceFilters(TransaccionesRepository.GetExchanges());
            SimboloFilterViewModel.ReplaceFilters(TransaccionesRepository.GetSymbols());
        }
        #endregion


        #region commands
        public ICommand AddTransaccionCommand { get; private set; }
        public ICommand EditTransaccionCommand { get; private set; }
        
        public ICommand ImportCdcAppCsvCommand { get; private set; }
        public ICommand ImportCdcExchangeCsvCommand { get; private set; }
        public ICommand ImportCdcSyndicateCsvCommand { get; private set; }
        public ICommand ImportCdcSuperchargerCsvCommand { get; private set; }
        public ICommand ImportEtherscanEthereumCsvCommand { get; private set; }
        public ICommand ImportEtherscanTokenCsvCommand { get; private set; }
        public ICommand ImportYoroiCsvCommand { get; private set; }

        public ICommand GoToFirstCommand { get; private set; }
        public ICommand GoToPreviousCommand { get; private set; }
        public RelayCommand<int> GoToPageCommand { get; private set; }
        public ICommand GoToNextCommand { get; private set; }
        public ICommand GoToLastCommand { get; private set; }
        private void WireCommands()
        {
            AddTransaccionCommand = new RelayCommand(() => AddTransaccion());
            EditTransaccionCommand = new RelayCommand(() => EditTransaccion());

            ImportCdcAppCsvCommand = new RelayCommand(() => ImportCdcAppCsv());
            ImportCdcExchangeCsvCommand = new RelayCommand(() => ImportCdcExchangeCsv());
            ImportCdcSyndicateCsvCommand = new RelayCommand(() => ImportCdcSyndicateCsv());
            ImportCdcSuperchargerCsvCommand = new RelayCommand(() => ImportCdcSuperchargerCsv());
            ImportEtherscanEthereumCsvCommand = new RelayCommand(() => ImportEtherscanEthereumCsv());
            ImportEtherscanTokenCsvCommand = new RelayCommand(() => ImportEtherscanTokenCsv());
            ImportYoroiCsvCommand = new RelayCommand(() => ImportYoroiCsv());

            GoToFirstCommand = new RelayCommand(() => GoToPage(1));
            GoToPreviousCommand = new RelayCommand(() => GoToPage(Math.Max((SelectedPage - 1), 1)));
            GoToPageCommand = new RelayCommand<int>((page) => GoToPage(page));
            GoToNextCommand = new RelayCommand(() => GoToPage(SelectedPage + 1));
            GoToLastCommand = new RelayCommand(() => GoToPage(MaxPage));
        }
        #endregion

        #region messenger
        private void RegisterMessenger()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<UpdateMessage>(this,
                (action) => Update(action.Restore));
        }
        #endregion


        public void Update(bool restore = false)
        {
            var transacciones = TransaccionesRepository.GetTransacciones(GetFilters())
                .OrderByDescending(transaccion => transaccion.Fecha)
                .ToList();

            int length = transacciones.Count();
            if (restore) SelectedPage = 1;
            SetPagination(length);

            int index = (SelectedPage - 1) * PageSize;
            int count = Math.Min(length - index, PageSize);
            transacciones = transacciones.GetRange(index, count);
            Transacciones.ReplaceRange(transacciones.Select(x => x.Adapt<TransaccionViewModel>()));
            // Ordenar
        }

        private TransaccionesFilter GetFilters()
        {
            var filters = new TransaccionesFilter();
            filters.Alerta = AlertaFilter;
            filters.Tipo = TipoFilterViewModel?.GetFilter();
            filters.Subtipo = SubtipoFilterViewModel?.GetFilter();
            filters.Exchange = ExchangeFilterViewModel?.GetFilter();
            filters.Simbolo = SimboloFilterViewModel?.GetFilter();
            return filters;
        }

        private void AddTransaccion()
        {
            var context = new EditTransaccionDialogViewModel();
            var view = new AddTransaccionDialog { DataContext = context };

            DialogHost.Show(view, "RootDialog", null,
                (s, e) =>
                {
                    if ((bool)e.Parameter == false) return;
                    Transacciones.Add(context.Transaccion);
                    Transaccion transaccion = context.Transaccion.Adapt<Transaccion>();
                    TransaccionesRepository.InsertTransaccion(transaccion);
                });
        }

        private void EditTransaccion()
        {
            var context = new EditTransaccionDialogViewModel((TransaccionViewModel)SelectedTransaccion.Clone());
            var view = new AddTransaccionDialog { DataContext = context };

            DialogHost.Show(view, "RootDialog", null,
                (s, e) =>
                {
                    if ((bool)e.Parameter == false) return;
                    TransaccionesRepository.DeleteTransaccion(SelectedTransaccion.Id);
                    Transacciones.Remove(SelectedTransaccion);
                    Transacciones.Add(context.Transaccion);
                    Transaccion transaccion = context.Transaccion.Adapt<Transaccion>();
                    TransaccionesRepository.InsertTransaccion(transaccion);
                });
        }


        #region imports
        private void ImportCdcAppCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadCryptoComAppCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }

        private void ImportCdcExchangeCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadCryptoComExchangeCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }

        private void ImportCdcSyndicateCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadCryptoComSyndicateCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }

        private void ImportCdcSuperchargerCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadCdcSuperchargerCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }


        private void ImportEtherscanEthereumCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadEtherscanEthereumCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }

        private void ImportEtherscanTokenCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadEtherscanTokenCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }

        private void ImportYoroiCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV (*.csv) | *.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadYoroiCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                TransaccionesRepository.InsertTransacciones(newTransacciones);
                Update();
            }
        }
        #endregion


        #region pagination
        private int selectedPage;
        public int SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                RaisePropertyChanged();
            }
        }

        private int pageSize;
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value;
                RaisePropertyChanged();
            }
        }

        private int maxPage;
        public int MaxPage
        {
            get => maxPage;
            set
            {
                maxPage = value;
                RaisePropertyChanged();
            }
        }

        public RangeObservableCollection<int> PageList { get; private set; }

        private void ConfigurePagination()
        {
            SelectedPage = 1;
            PageSize = 50;

            PageList = new RangeObservableCollection<int>();
        }


        public void SetPagination(int length)
        {
            if (length > 0) MaxPage = (length / PageSize) + 1;
            else MaxPage = 1;

            var firstPage = Math.Max(1, SelectedPage - 5);
            var pagelist = Enumerable.Range(firstPage, Math.Min(MaxPage - SelectedPage + 6, 10));
            PageList.ReplaceRange(pagelist);

            if (SelectedPage > MaxPage) SelectedPage = MaxPage;
        }


        private void GoToPage(int page)
        {
            SelectedPage = page;
            Update();
        }
        #endregion
    }
}
