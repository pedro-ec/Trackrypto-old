using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Factories.CryptoComApp.Utils;

namespace Trackrypto.Model.Factories.CryptoComApp.Strategies
{
    class BaseStrategy : ITransaccionFactoryStrategy
    {
        public virtual Transaccion GetTransaccion(object data)
        {
            CsvRow csvRow = (CsvRow)data;
            Transaccion transaccion = new Transaccion
            {
                Tipo = "",
                Subtipo = "",
                Exchange = "crypto.com_app",
                Cantidad_Comision = 0,
                Divisa_Comision = "",
                Comentarios = csvRow.TransactionDescription,
                Fecha = csvRow.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = ""
            };

            if (csvRow.NativeAmount != null)
                if (string.Equals(csvRow.NativeCurrency, "EUR"))
                    transaccion.Valor_Eur = csvRow.NativeAmount;

            if (csvRow.Amount > 0)
            {
                transaccion.Cantidad_Compra = csvRow.Amount;
                transaccion.Divisa_Compra = csvRow.Currency;
            }

            if (csvRow.Amount < 0)
            {
                transaccion.Cantidad_Venta = -csvRow.Amount;
                transaccion.Divisa_Venta = csvRow.Currency;
                if (!string.IsNullOrWhiteSpace(csvRow.ToCurrency))
                {
                    transaccion.Cantidad_Compra = csvRow.ToAmount;
                    transaccion.Divisa_Compra = csvRow.ToCurrency;
                }
            }

            return transaccion;
        }
    }
}
