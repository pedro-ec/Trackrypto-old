using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    public class SpotWalletCsvRow
    {
        public long OrderId;
        public long TradeId;
        public DateTime TimestampUTC;
        public string Symbol;
        public string Side;
        public decimal? TradedPrice;
        public decimal? TradedQuantity;
        public decimal? Volume;
        public decimal? Fee;
        public string FeeCurrency;

        public static SpotWalletCsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            SpotWalletCsvRow csvRow = new SpotWalletCsvRow
            {
                OrderId = Convert.ToInt64(values[0]),
                TradeId = Convert.ToInt64(values[1]),
                TimestampUTC = Convert.ToDateTime(values[2]),
                Symbol = values[3],
                Side = values[4],
                TradedPrice = Convert.ToDecimal(values[5], CultureInfo.InvariantCulture),
                TradedQuantity = Convert.ToDecimal(values[6], CultureInfo.InvariantCulture),
                Volume = Convert.ToDecimal(values[7], CultureInfo.InvariantCulture),
                Fee = Convert.ToDecimal(values[8], CultureInfo.InvariantCulture),
                FeeCurrency = values[9]
            };
            return csvRow;
        }
    }
}
