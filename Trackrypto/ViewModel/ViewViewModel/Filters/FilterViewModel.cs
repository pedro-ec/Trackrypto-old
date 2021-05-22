using GalaSoft.MvvmLight;
using HMY.Helpers.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.ViewModel.Messenger;

namespace Trackrypto.ViewModel.ViewViewModel.Filters
{
    public class FilterViewModel : ViewModelBase
    {
        public string Title { get; }
        public RangeObservableCollection<FilterItemViewModel> Filters { get; }

        public bool? ShowAll
        {
            get
            {
                if (Filters.All(filter => filter.Selected))
                    return true;

                if (!Filters.Any(filter => filter.Selected))
                    return false;

                return null;
            }

            set
            {
                if (value == null) 
                    return;

                foreach (var filter in Filters) 
                    filter.SetWithoutUpdate(value ?? true);

                RaisePropertyChanged();
                Update();
            }
        }

        public FilterViewModel(string title)
        {
            Title = title;
            
            Filters = new RangeObservableCollection<FilterItemViewModel>();
        }

        public void ReplaceFilters(string[] filters)
        {
            foreach (var filter in Filters)
                filter.PropertyChanged -= Filter_PropertyChanged;

            Filters.ReplaceRange(filters.Select(filter => new FilterItemViewModel(filter)));

            foreach (var filter in Filters)
                filter.PropertyChanged += Filter_PropertyChanged;
        }

        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(ShowAll));
        }

        private void Update()
        {
            var msg = new UpdateMessage() { Restore = true };
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(msg);
        }

        public string[] GetFilter() =>
            Filters
            .Where(filter => filter.Selected)
            .Select(filter => filter.Value)
            .ToArray();
    }
}
