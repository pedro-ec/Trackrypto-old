using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComSyndicate
{
    public class CsvRow
    {
        public DateTime TimestampUTC;
        public decimal? CroDeposit;
        public decimal? CroReturn;
        public decimal? CroSpent;
        public string ToCurrency;
        public decimal? ToAmount;

        public static CsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            CsvRow csvRow = new CsvRow
            {
                TimestampUTC = Convert.ToDateTime(values[0]),
                CroDeposit = string.IsNullOrWhiteSpace(values[1]) ? 0 : Convert.ToDecimal(values[1], CultureInfo.InvariantCulture),
                CroReturn = string.IsNullOrWhiteSpace(values[2]) ? 0 : Convert.ToDecimal(values[2], CultureInfo.InvariantCulture),
                CroSpent = string.IsNullOrWhiteSpace(values[3]) ? 0 : Convert.ToDecimal(values[3], CultureInfo.InvariantCulture),
                ToCurrency = values[4],
                ToAmount = string.IsNullOrWhiteSpace(values[5]) ? 0 : Convert.ToDecimal(values[5], CultureInfo.InvariantCulture),
            };
            return csvRow;
        }
    }
}
