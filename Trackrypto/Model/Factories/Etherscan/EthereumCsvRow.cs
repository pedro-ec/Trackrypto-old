using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.Etherscan
{
    public class EthereumCsvRow
    {
        public string Txhash;
        public int? Blockno;
        public int? UnixTimestamp;
        public DateTime TimestampUTC;
        public string From;
        public string To;
        public string ContractAddress;
        public decimal? Value_IN_ETH;
        public decimal? Value_OUT_ETH;
        public decimal? CurrentValue;
        public decimal? TxnFeeETH;
        public decimal? TxnFeeUSD;
        public decimal? HistoricalPrice;
        public string Status;
        public string ErrCode;

        public static EthereumCsvRow FromCsv(string csvLine)
        {
            List<string> values = new List<string>(); 
            csvLine.Split(',').ToList().ForEach(value => values.Add(value.Trim('\"')));
  
            EthereumCsvRow csvRow = new EthereumCsvRow
            {
                Txhash = values[0],
                Blockno = Convert.ToInt32(values[1]),
                UnixTimestamp = Convert.ToInt32(values[2]),
                TimestampUTC = Convert.ToDateTime(values[3]),
                From = values[4],
                To = values[5],
                ContractAddress = values[6],
                Value_IN_ETH = Convert.ToDecimal(values[7], CultureInfo.InvariantCulture),
                Value_OUT_ETH = Convert.ToDecimal(values[8], CultureInfo.InvariantCulture),
                CurrentValue = Convert.ToDecimal(values[9], CultureInfo.InvariantCulture),
                TxnFeeETH = Convert.ToDecimal(values[10], CultureInfo.InvariantCulture),
                TxnFeeUSD = Convert.ToDecimal(values[11], CultureInfo.InvariantCulture),
                HistoricalPrice = Convert.ToDecimal(values[12], CultureInfo.InvariantCulture),
                Status = values[13],
                ErrCode = values[14]
            };
            return csvRow;
        }
    }
}
