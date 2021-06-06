using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComSupercharger
{
    public class CsvRow
    {
        public DateTime TimestampUTC;
        public string Currency;
        public decimal? Amount;
        public string Exchange;

        public static CsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            CsvRow csvRow = new CsvRow
            {
                TimestampUTC = Convert.ToDateTime(values[0]),
                Currency = values[1],
                Amount = string.IsNullOrWhiteSpace(values[2]) ? 0 : Convert.ToDecimal(values[2], CultureInfo.InvariantCulture),
                Exchange = values[3]
            };
            return csvRow;
        }
    }
}
