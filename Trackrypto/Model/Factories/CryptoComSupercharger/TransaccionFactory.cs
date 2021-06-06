using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model.Factories.CryptoComSupercharger
{
    public static class TransaccionFactory
    {
        public static Transaccion GetTransaccion(string line)
        {
            CsvRow row = CsvRow.FromCsv(line);

            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "ingreso",
                Subtipo = "supercharger",
                Exchange = row.Exchange,
                Cantidad_Compra = row.Amount,
                Divisa_Compra = row.Currency,
                Detalles = $"Supercharger {row.Currency}",
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = ""
            };

            return transaccion;
        }
    }
}
