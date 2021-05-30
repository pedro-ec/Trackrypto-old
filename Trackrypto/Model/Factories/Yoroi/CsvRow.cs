using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.Yoroi
{
    public class CsvRow
    {
        public string Type;
        public decimal? BuyAmount;
        public string BuyCurrency;
        public decimal? SellAmount;
        public string SellCurrency;
        public decimal? FeeAmount;
        public string FeeCurrency;
        public string Exchange;
        public string TradeGroup;
        public string Comment;
        public DateTime TimestampUTC;


        public static CsvRow FromCsv(string csvLine)
        {
            List<string> values = new List<string>();
            csvLine.Split(',').ToList().ForEach(value => values.Add(value.Trim('\"')));

            CsvRow csvRow = new CsvRow
            {
                Type = values[0],
                BuyAmount = string.IsNullOrWhiteSpace(values[1]) ? 0 : Convert.ToDecimal(values[1], CultureInfo.InvariantCulture),
                BuyCurrency = values[2],
                SellAmount = string.IsNullOrWhiteSpace(values[3]) ? 0 : Convert.ToDecimal(values[3], CultureInfo.InvariantCulture),
                SellCurrency = values[4],
                FeeAmount = string.IsNullOrWhiteSpace(values[5]) ? 0 : Convert.ToDecimal(values[5], CultureInfo.InvariantCulture),
                FeeCurrency = values[6],
                Exchange = values[7],
                TradeGroup = values[8],
                Comment = values[9],
                TimestampUTC = Convert.ToDateTime(values[10])            
            };
            return csvRow;
        }
    }
}
