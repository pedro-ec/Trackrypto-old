using HMY.Helpers.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities.Summary;

namespace Trackrypto.ViewModel.EntityViewModel.Summary
{
    public class ExchangeSummaryViewModel 
    {
        public string Name { get; }
        public RangeObservableCollection<CoinSummaryViewModel> Coins { get; }
        
        public ExchangeSummaryViewModel(string name)
        {
            Name = name;
            Coins = new RangeObservableCollection<CoinSummaryViewModel>();
        }

        public ExchangeSummaryViewModel(ExchangeSummary exchangeSummary)
        {
            if (exchangeSummary == null) return;

            Name = exchangeSummary.Name;
            Coins = new RangeObservableCollection<CoinSummaryViewModel>(exchangeSummary.Coins.Select(coin => new CoinSummaryViewModel(coin)));
        }
    }
}
