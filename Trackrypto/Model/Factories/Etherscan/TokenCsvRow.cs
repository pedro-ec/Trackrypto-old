using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.Etherscan
{
    public class TokenCsvRow
    {
        public string Txhash;
        public int? UnixTimestamp;
        public DateTime TimestampUTC;
        public string From;
        public string To;
        public decimal? Value;
        public string ContractAddress;
        public string TokenName;
        public string TokenSymbol;


        public static TokenCsvRow FromCsv(string csvLine)
        {
            List<string> values = new List<string>();
            csvLine.Split(',').ToList().ForEach(value => values.Add(value.Trim('\"')));

            TokenCsvRow csvRow = new TokenCsvRow
            {
                Txhash = values[0],
                UnixTimestamp = Convert.ToInt32(values[1]),
                TimestampUTC = Convert.ToDateTime(values[2]),
                From = values[3],
                To = values[4],
                Value = Convert.ToDecimal(values[5], CultureInfo.InvariantCulture),
                ContractAddress = values[6],
                TokenName = values[7],
                TokenSymbol = values[8]
            };
            return csvRow;
        }
    }
}
