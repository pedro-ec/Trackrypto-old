using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Entities.Summary
{
    public class CoinSummary
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
        public decimal Value { get; set; }

        public CoinSummary(string symbol, decimal value = 0)
        {
            Symbol = symbol;
            Value = value;
        }

    }
}
