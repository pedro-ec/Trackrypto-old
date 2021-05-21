using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Entities.Summary
{
    public class ExchangeSummary
    {
        public string Name { get; }
        public List<CoinSummary> Coins { get; }

        public ExchangeSummary(string name)
        {
            Name = name;
            Coins = new List<CoinSummary>();
        }
    }
}
