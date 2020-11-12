using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Factories;
using Trackrypto.Model.Factories.CryptoComApp;

namespace Trackrypto.Utils
{
    public static class FileLoader
    {
        public static Transaccion[] LoadCryptoComCsv(string filename)
        {
            var reader = new StreamReader(File.OpenRead(filename));
            List<Transaccion> transacciones = new List<Transaccion>();
            var _ = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var transaccion = CryptoComTransaccionFactory.FromAppCsv(line);
                if (transaccion == null)
                {
                    // error
                    Debug.WriteLine("transaccion = null");
                    continue;
                }
                transacciones.Add(transaccion);
            }
            //TransaccionRepository.BulkInsertTransaccion(transacciones.ToArray());
            return transacciones.ToArray();
        }
    }
}
