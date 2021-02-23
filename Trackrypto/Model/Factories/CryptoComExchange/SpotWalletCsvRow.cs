using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    public class SpotWalletCsvRow
    {
        public string AccountType;
        public int OrderId;
        public int TradeId;
        public DateTime TimestampUTC;
        public string Symbol;
        public string Side;
        public string LiquidityIndicator;
        public decimal? TradedPrice;
        public decimal? TradedQuantity;
        public decimal? Fee;
        public string FeeCurrency;

        public static SpotWalletCsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            SpotWalletCsvRow csvRow = new SpotWalletCsvRow
            {
                AccountType = values[0],
                OrderId = Convert.ToInt32(values[1]),
                TradeId = Convert.ToInt32(values[2]),
                TimestampUTC = Convert.ToDateTime(values[3]),
                Symbol = values[4],
                Side = values[5],
                LiquidityIndicator = values[6],
                TradedPrice = Convert.ToDecimal(values[7]),
                TradedQuantity = Convert.ToDecimal(values[8]),
                Fee = Convert.ToDecimal(values[9]),
                FeeCurrency = values[10],
         
            };
            return csvRow;
        }
    }
}
