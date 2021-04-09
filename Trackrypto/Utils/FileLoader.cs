using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Factories;

namespace Trackrypto.Utils
{
    public static class FileLoader
    {
        public static Transaccion[] LoadCryptoComAppCsv(string filename)
        {
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = Model.Factories.CryptoComApp.TransaccionFactory.GetTransaccion(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }


        private static readonly Dictionary<string, Func<string, Transaccion>> CryptoComExchangeFactories = new Dictionary<string, Func<string, Transaccion>>()
        {
            { "DEPOSIT.csv", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetDeposito(filename) },
            { "WITHDRAWAL.csv", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetRetirada(filename) },
            { "STAKE_INTEREST.csv", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetStake(filename) },
            { "TRADE_FEE_REBATE.csv", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetReembolso(filename) },
            { "SOFT_STAKE_INTEREST.csv", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetSoftStaking(filename) },
            { "SPOT_TRADE.csv", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetSpotTrade(filename) },
        };

        public static Transaccion[] LoadCryptoComExchangeCsv(string path, string filename)
        {
            Func<string, Transaccion> getTransaccion;
            CryptoComExchangeFactories.TryGetValue(filename, out getTransaccion);
            if (getTransaccion == null) return null;

            var reader = new StreamReader(File.OpenRead(path));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = getTransaccion(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }
    }
}
