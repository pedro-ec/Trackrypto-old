using GalaSoft.MvvmLight;
using HMY.Helpers.Collections;
using HMY.Infrastructure.AsyncResponse;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Trackrypto.Repositories;
using Trackrypto.ViewModel.EntityViewModel;
using Trackrypto.ViewModel.Navigation;

namespace Trackrypto.ViewModel.ViewViewModel
{
    public class TransactionListViewModel : ViewModelBase, IPageViewModel
    {
        #region private
        private int selectedPage;
        private int pageSize;
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

        private void WireCommands()
        {

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
    }
}
