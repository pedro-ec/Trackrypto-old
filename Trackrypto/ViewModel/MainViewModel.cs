using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Trackrypto.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region private
        private bool showSummary;
        private bool showTransactionList;
        #endregion

        public bool ShowSummary
        {
            get => showSummary;
            set
            {
                showSummary = value;
                RaisePropertyChanged();
            }
        }
        public bool ShowTransactionList
        {
            get => showTransactionList;
            set
            {
                showTransactionList = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            WireCommands();
        }

        #region comands
        public RelayCommand<string> ToggleViewCommmand { get; private set; }
        private void WireCommands()
        {
            ToggleViewCommmand = new RelayCommand<string>((view) => ToggleView(view));
        }
        #endregion

        private void ToggleView(string view)
        {
            ShowSummary = string.Equals(view, "Summary");
            ShowTransactionList = string.Equals(view, "TransactionList");
        }
    }
}
