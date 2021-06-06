using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    Debug.WriteLine("transaccion 0 = null");
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
                    Debug.WriteLine("transaccion 1 = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }

        public static Transaccion[] LoadCryptoComSyndicateCsv(string filename)
        {
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = Model.Factories.CryptoComSyndicate.TransaccionFactory.GetTransaccion(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion 4 = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }

        public static Transaccion[] LoadCdcSuperchargerCsv(string filename)
        {
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = Model.Factories.CryptoComSupercharger.TransaccionFactory.GetTransaccion(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion 6 = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }

        public static Transaccion[] LoadEtherscanEthereumCsv(string filename)
        {
            //string key = GetEtherscanKey(filename);
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = Model.Factories.Etherscan.TransaccionFactory.GetEthereumTransaccion(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion 2 = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }

        public static Transaccion[] LoadEtherscanTokenCsv(string filename)
        {
            string key = GetEtherscanKey(filename);
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = Model.Factories.Etherscan.TransaccionFactory.GetTokenTransaccion(line, key);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion 3 = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }

        private static string GetEtherscanKey(string filename)
        {
            Regex rx = new Regex("0x.{40}");
            var keyMatch = rx.Match(filename);
            return keyMatch.Value;
        }


        public static Transaccion[] LoadYoroiCsv(string filename)
        {
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = Model.Factories.Yoroi.TransaccionFactory.GetTransaccion(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion 5 = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            return transacciones.ToArray();
        }
    }
}
