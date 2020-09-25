using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComApp.Utils
{
    public class CsvRow
    {
        public DateTime TimestampUTC;
        public string TransactionDescription;
        public string Currency;
        public decimal? Amount;
        public string ToCurrency;
        public decimal? ToAmount;
        public string NativeCurrency;
        public decimal? NativeAmount;
        public decimal? NativeAmountUSD;
        public string TransactionKind;

        public static CsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            CsvRow csvRow = new CsvRow
            {
                TimestampUTC = Convert.ToDateTime(values[0]),
                TransactionDescription = values[1],
                Currency = values[2],
                Amount = string.IsNullOrWhiteSpace(values[3]) ? 0 : Convert.ToDecimal(values[3], CultureInfo.InvariantCulture),
                ToCurrency = values[4],
                ToAmount = string.IsNullOrWhiteSpace(values[5]) ? 0 : Convert.ToDecimal(values[5], CultureInfo.InvariantCulture),
                NativeCurrency = values[6],
                NativeAmount = string.IsNullOrWhiteSpace(values[7]) ? 0 : Convert.ToDecimal(values[7], CultureInfo.InvariantCulture),
                NativeAmountUSD = Convert.ToDecimal(values[8]),
                TransactionKind = values[9]
            };
            return csvRow;
        }
    }
}
