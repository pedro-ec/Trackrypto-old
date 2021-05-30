using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model.Factories.Yoroi
{
    public static class TransaccionFactory
    {
        private static Dictionary<string, string> translations = new Dictionary<string, string>
        {
            { "Deposit", "deposito" },
            { "Withdrawal", "retiro" }
        };

        public static Transaccion GetTransaccion(string line)
        {
            CsvRow row = CsvRow.FromCsv(line);

            bool verifiedType = translations.ContainsKey(row.Type);

            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = verifiedType ? translations[row.Type] : "operacion",
                Exchange = String.IsNullOrWhiteSpace(row.Exchange) ? "Yoroi" : row.Exchange,
                Cantidad_Compra = row.BuyAmount,
                Divisa_Compra = row.BuyCurrency,
                Cantidad_Venta = row.SellAmount,
                Divisa_Venta = row.SellCurrency,
                Cantidad_Comision = row.FeeAmount,
                Divisa_Comision = row.FeeCurrency,
                Detalles = row.TradeGroup,
                Comentarios = row.Comment,
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = ""
            };

            if (!verifiedType)
            {
                transaccion.Alerta = true;
                if (!string.IsNullOrWhiteSpace(transaccion.Mensaje_Alerta)) 
                    transaccion.Mensaje_Alerta += "\n";
                transaccion.Mensaje_Alerta += "Tipo no reconocido";
            }

            return transaccion;
        }
    }
}
