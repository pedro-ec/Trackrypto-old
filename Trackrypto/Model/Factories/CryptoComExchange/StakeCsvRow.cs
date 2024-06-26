﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    [Obsolete("Desde 2023 solo hay un CSV", error: true)]
    public class StakeCsvRow
    {
        public DateTime TimestampUTC;
        public string StakeCurrency;
        public decimal? StakeAmount;
        public string Apr;
        public string InterestCurrency;
        public decimal? InterestAmount;
        public string Status;

        public static StakeCsvRow FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            StakeCsvRow csvRow = new StakeCsvRow
            {
                TimestampUTC = Convert.ToDateTime(values[0]),
                StakeCurrency = values[1],
                StakeAmount = Convert.ToDecimal(values[2], CultureInfo.InvariantCulture),
                Apr = values[3],
                InterestCurrency = values[4],
                InterestAmount = Convert.ToDecimal(values[5], CultureInfo.InvariantCulture),
                Status = values[6]
            };
            return csvRow;
        }
    }
}
