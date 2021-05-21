using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities.Summary;

namespace Trackrypto.ViewModel.EntityViewModel.Summary
{
    public class CoinSummaryViewModel
    {
        private Dictionary<DateTime, decimal> cummulatedValue;
        private Dictionary<DateTime, decimal> NewCummulatedValue()
        {
            cummulatedValue = new Dictionary<DateTime, decimal>();
            return cummulatedValue;
        }

        public Dictionary<DateTime, decimal> CummulatedValue
        {
            get
            {
                return cummulatedValue ?? NewCummulatedValue();
            }
        }

        public string Symbol { get; }
        public decimal Value { get; }

        public CoinSummaryViewModel(CoinSummary coinSummary)
        {
            Symbol = coinSummary.Symbol;
            Value = coinSummary.Value;
            cummulatedValue = coinSummary.CummulatedValue;
        }

        public CoinSummaryViewModel(string symbol, decimal value = 0)
        {
            Symbol = symbol;
            Value = value;
        }
    }
}
