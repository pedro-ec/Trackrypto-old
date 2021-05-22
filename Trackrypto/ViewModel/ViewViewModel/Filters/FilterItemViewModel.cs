using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.ViewModel.Messenger;

namespace Trackrypto.ViewModel.ViewViewModel.Filters
{
    public class FilterItemViewModel : ViewModelBase
    {
        private bool updateFlag;

        private bool selected;
        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                RaisePropertyChanged();
                Update();
            }
        }

        public string Value { get; set; }


        public FilterItemViewModel(string value)
        {
            Value = value;
            SetWithoutUpdate(true);
        }


        public void SetWithoutUpdate(bool newSelected)
        {
            updateFlag = false;
            Selected = newSelected;
            updateFlag = true;
        }

        private void Update()
        {
            if (updateFlag)
            {
                var msg = new UpdateMessage() { Restore = true };
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(msg);
            }
        }
    }
}
