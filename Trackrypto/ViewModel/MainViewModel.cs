using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackrypto.ViewModel.Navigation;
using Trackrypto.ViewModel.ViewViewModel;

namespace Trackrypto.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region private
        private IPageViewModel currentPageViewModel;
        private Dictionary<string, IPageViewModel> pageViewModels = new Dictionary<string, IPageViewModel>();
        #endregion


        public Dictionary<string, IPageViewModel> PageViewModels => pageViewModels;

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return currentPageViewModel;
            }
            set
            {
                currentPageViewModel = value;
                RaisePropertyChanged();
            }
        }


        public MainViewModel()
        {
            AddViewModels();

            WireCommands();
        }


        #region comands
        public RelayCommand<string> ToggleViewCommand { get; private set; }
        private void WireCommands()
        {
            ToggleViewCommand = new RelayCommand<string>((view) => ToggleView(view));
        }
        #endregion


        #region navigation
        private void AddViewModels()
        {
            pageViewModels.Add("Summary", new SummaryViewModel());
            pageViewModels.Add("TransactionList", new TransactionListViewModel());

            CurrentPageViewModel = PageViewModels["Summary"];
        }

        private void ToggleView(string view)
        {
            if (PageViewModels.ContainsKey(view))
                CurrentPageViewModel = PageViewModels[view];
        }
        #endregion
    }
}
