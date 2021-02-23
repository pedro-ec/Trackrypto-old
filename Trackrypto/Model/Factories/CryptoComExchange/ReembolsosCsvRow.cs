using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    public class ReembolsosCsvRow
    {
        public DateTime TimestampUTC;
        public string TradeFeeCurrency;
        public decimal? TradeFeeAmount;
        public string RebatePercentage;
        public string RebateCurrency;
        public decimal? RebateAmount;
        public string RebateDestination;
        public int? Status;

        public static ReembolsosCsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            ReembolsosCsvRow csvRow = new ReembolsosCsvRow
            {
                TimestampUTC = Convert.ToDateTime(values[0]),
                TradeFeeCurrency = values[1],
                TradeFeeAmount = Convert.ToDecimal(values[2]),
                RebatePercentage = values[3],
                RebateCurrency = values[4],
                RebateAmount = Convert.ToDecimal(values[5]),
                RebateDestination = values[6],
                Status = Convert.ToInt32(values[7])
            };
            return csvRow;
        }
    }
}
