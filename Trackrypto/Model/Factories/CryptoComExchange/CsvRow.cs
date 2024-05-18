using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    public class CsvRow
    {
        public long JournalID;
        public DateTime TimestampUTC;
        public string JournalType;
        public string Instrument;
        public decimal TransactionQuantity;
        public string ClientOrderId;

        public static CsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            CsvRow csvRow = new CsvRow
            {
                JournalID = long.Parse(values[0]),
                TimestampUTC = Convert.ToDateTime(values[1]),
                JournalType = values[3],
                Instrument = values[4],
                TransactionQuantity = Convert.ToDecimal(values[7], CultureInfo.InvariantCulture),
                ClientOrderId = values[13]
            };
            return csvRow;
        }
    }
}
