using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HMY.Helpers.Collections;
using HMY.Infrastructure.AsyncResponse;
using Mapster;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Trackrypto.Model.Entities;
using Trackrypto.Repositories;
using Trackrypto.Utils;
using Trackrypto.View.Dialogs;
using Trackrypto.ViewModel.EntityViewModel;
using Trackrypto.ViewModel.Navigation;

namespace Trackrypto.ViewModel.ViewViewModel
{
    public class TransactionListViewModel : ViewModelBase, IPageViewModel
    {
        #region private
        private int selectedPage;
        //private int pageSize;
        #endregion


        public int SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                RaisePropertyChanged();
            }
        }

        public RangeObservableCollection<TransaccionViewModel> Transacciones { get; set; }
        public CollectionViewSource TransaccionesViewSource { get; set; }

        #region constructor
        public TransactionListViewModel()
        {
            Transacciones = new RangeObservableCollection<TransaccionViewModel>();
            TransaccionesViewSource = new CollectionViewSource { Source = Transacciones };

            WireCommands();
        }
        #endregion

        #region commands
        public ICommand AddTransaccionCommand { get; private set; }
        public ICommand LoadFileCommand { get; private set; }
        private void WireCommands()
        {
            AddTransaccionCommand = new RelayCommand(() => AddTransaccion());
            LoadFileCommand = new RelayCommand(() => LoadFile());
        }
        #endregion

        #region messenger

        #endregion

        public void OnNavigate()
        {
            Update();
        }

        public void Update()
        {
            var response = TransaccionRepository.GetTransacciones();
            if (response.Type != ResponseType.Ok) return;
            var newTransacciones = response.Data;

            Transacciones.ReplaceRange(newTransacciones.Select(x => x.Adapt<TransaccionViewModel>()));
        }

        private void AddTransaccion()
        {
            var context = new EditTransaccionDialogViewModel();
            var view = new AddTransaccionDialog { DataContext = context };

            DialogHost.Show(view, "RootDialog", null,
                (s, e) =>
                {
                    if ((bool)e.Parameter == false) return;

                    Transaccion transaccion = context.Transaccion.Adapt<Transaccion>();
                    TransaccionRepository.InsertTransaccion(transaccion);
                    Update();
                });
        }

        private void LoadFile()
        {
            FileLoader.LoadCryptoComCsv("");
        }
    }
}
