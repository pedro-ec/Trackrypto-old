using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model.Factories.CryptoComSyndicate
{
    public static class TransaccionFactory
    {
        public static Transaccion GetTransaccion(string line)
        {
            CsvRow row = CsvRow.FromCsv(line);

            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "operacion",
                Exchange = "crypto.com_exchange",
                Cantidad_Compra = row.ToAmount,
                Divisa_Compra = row.ToCurrency,
                Cantidad_Venta = row.CroSpent,
                Divisa_Venta = "CRO",
                Detalles = "Syndicate",
                Comentarios = $"Deposited: {row.CroDeposit} CRO",
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = ""
            };

            return transaccion;
        }
    }
}
