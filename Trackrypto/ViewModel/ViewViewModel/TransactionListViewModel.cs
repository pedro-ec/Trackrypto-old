using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HMY.Helpers.Collections;
using HMY.Infrastructure.AsyncResponse;
using Mapster;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Trackrypto.Model;
using Trackrypto.Model.Entities;
using Trackrypto.Utils;
using Trackrypto.View.Dialogs;
using Trackrypto.ViewModel.EntityViewModel;
using Trackrypto.ViewModel.Messenger;
using Trackrypto.ViewModel.Navigation;

namespace Trackrypto.ViewModel.ViewViewModel
{
    public class TransactionListViewModel : ViewModelBase, IPageViewModel
    {
        #region private
        private Domain model;
        #endregion        

        public RangeObservableCollection<TransaccionViewModel> Transacciones { get; set; }

        #region constructor
        public TransactionListViewModel()
        {
            model = Domain.GetModel();

            Transacciones = new RangeObservableCollection<TransaccionViewModel>();

            WireCommands();
            RegisterMessenger();
            ConfigurePagination();

            Update();
        }
        #endregion

        #region commands
        public ICommand AddTransaccionCommand { get; private set; }
        public ICommand ImportCryptoComCsvCommand { get; private set; }
        public RelayCommand<int> GoToPageCommand { get; private set; }
        private void WireCommands()
        {
            AddTransaccionCommand = new RelayCommand(() => AddTransaccion());
            ImportCryptoComCsvCommand = new RelayCommand(() => ImportCryptoComCsv());
            GoToPageCommand = new RelayCommand<int>((page) => GoToPage(page));
        }
        #endregion

        #region messenger
        private void RegisterMessenger()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<UpdateMessage>(this,
                (action) => Update());
        }
        #endregion


        public void Update()
        {
            var transacciones = model.GetTransacciones().ToList();
            // Filtrar
            int length = transacciones.Count();
            SetMaxPage(length);
            //if (length == 0)
            //{
            //    Transacciones.Clear();
            //    return;
            //}
            if (SelectedPage > MaxPage) SelectedPage = MaxPage;
            int index = (SelectedPage - 1) * PageSize;
            int count = Math.Min(length - index, PageSize);
            transacciones = transacciones.GetRange(index, count);
            Transacciones.ReplaceRange(transacciones.Select(x => x.Adapt<TransaccionViewModel>()));
            // Ordenar
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
                    model.InsertTransaccion(transaccion);
                });
        }


        #region imports
        private void ImportCryptoComCsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv) | *.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                var newTransacciones = FileLoader.LoadCryptoComCsv(openFileDialog.FileName);
                // Añadir diálogo de revisión
                model.InsertTransacciones(newTransacciones);
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
            PageSize = 10;

            PageList = new RangeObservableCollection<int>();
        }


        public void SetMaxPage(int length)
        {
            if (length > 0) MaxPage = (length / PageSize) + 1;
            else MaxPage = 1;
            PageList.ReplaceRange(Enumerable.Range(1, MaxPage));
        }

        private void GoToPage(int page)
        {
            SelectedPage = page;
            Update();
        }
        #endregion
    }
}
