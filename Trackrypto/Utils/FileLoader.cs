using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.Repositories;

namespace Trackrypto.Utils
{
    public static class FileLoader
    {
        public static void LoadCryptoComCsv(string file)
        {
            Transaccion[] transacciones = new Transaccion[]
            {
                new Transaccion {Comentarios = "trans1"},
                new Transaccion {Comentarios = "trans2"}
            };
            TransaccionRepository.BulkInsertTransaccion(transacciones);
        }
    }
}
