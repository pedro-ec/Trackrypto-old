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
            { "DEPOSIT", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetDeposito(filename) },
            { "WITHDRAWAL", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetRetirada(filename) },
            { "STAKE_INTEREST", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetRetirada(filename) },
            { "TRADE_FEE_REBATE", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetRetirada(filename) },
            { "SOFT_STAKE_INTEREST", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetRetirada(filename) },
            { "SPOT_TRADE", (filename) => Model.Factories.CryptoComExchange.TransaccionFactory.GetRetirada(filename) },
        };

        public static Transaccion[] LoadCryptoComExchangeCsv(string path, string filename)
        {
            var getTransaccion = CryptoComExchangeFactories[filename];
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
