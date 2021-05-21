using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HMY.Helpers.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackrypto.Model;
using Trackrypto.Repositories;
using Trackrypto.ViewModel.EntityViewModel;
using Trackrypto.ViewModel.EntityViewModel.Summary;
using Trackrypto.ViewModel.Navigation;

namespace Trackrypto.ViewModel.ViewViewModel
{
    public class SummaryViewModel : ViewModelBase, IPageViewModel
    {
        #region private
        private readonly Domain model;
        #endregion

        public RangeObservableCollection<ExchangeSummaryViewModel> Exchanges { get; }

        #region constructor
        public SummaryViewModel()
        {
            model = Domain.GetModel();

            Exchanges = new RangeObservableCollection<ExchangeSummaryViewModel>();

            WireCommands();

            Update();
        }
        #endregion

        #region commands
        public ICommand UpdateCommand { get; private set; }
        private void WireCommands()
        {
            UpdateCommand = new RelayCommand(() => Update());
        }
        #endregion

        public void Update(bool restore = false)
        {
            Exchanges.ReplaceRange(SummaryRepository
                .GetSummary()
                .Select(exchange => new ExchangeSummaryViewModel(exchange)));
        }
    }
}
