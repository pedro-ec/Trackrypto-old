using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    public class DepositosRetirosCsvRow
    {
        public DateTime TimestampUTC;
        public string Currency;
        public decimal? Amount;
        public decimal? Fee;
        public string Address;
        public int? Status;

        public static DepositosRetirosCsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            //DepositosRetirosCsvRow csvRow = new DepositosRetirosCsvRow
            //{
            //    TimestampUTC = Convert.ToDateTime(values[0]),
            //    Currency = values[1],
            //    Amount = Convert.ToDecimal(string.Concat("0", values[2])),
            //    Fee = Convert.ToDecimal(string.Concat("0", values[3])),
            //    Address = values[4],
            //    Status = Convert.ToInt32(values[5])
            //};


            //TODO CultureInfo en ToDecimal
            DepositosRetirosCsvRow csvRow = new DepositosRetirosCsvRow();
            csvRow.TimestampUTC = Convert.ToDateTime(values[0]);
            csvRow.Currency = values[1];
            csvRow.Amount = Convert.ToDecimal(string.Concat("0", values[2]), CultureInfo.InvariantCulture);
            csvRow.Fee = Convert.ToDecimal(string.Concat("0", values[3]), CultureInfo.InvariantCulture);
            csvRow.Address = values[4];
            csvRow.Status = Convert.ToInt32(values[5]);
            return csvRow;
        }
    }
}
