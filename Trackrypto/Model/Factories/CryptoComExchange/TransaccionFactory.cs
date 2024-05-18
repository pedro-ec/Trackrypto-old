using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model.Factories.CryptoComExchange
{
    public static class TransaccionFactory
    {
        public static Transaccion GetTransaccion(string line)
        {
            CsvRow row = CsvRow.FromCsv(line);

            if (row.JournalType.Equals("SOFT_STAKE_REWARD")) return CreateSoftStaking(row);
            if (row.JournalType.EndsWith("WITHDRAWAL")) return CreateRetirada(row);
            throw new NotImplementedException();
        }


        private static Transaccion CreateDeposito(CsvRow row)
        {
            throw new NotImplementedException();
        }


        private static Transaccion CreateRetirada(CsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Venta = row.Instrument,
                Cantidad_Venta = - row.TransactionQuantity,
                Divisa_Comision = "",
                Cantidad_Comision = 0,
                Detalles = row.JournalType,
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "retirada"
            };
            return transaccion;
        }


        private static Transaccion CreateStake(CsvRow row)
        {
            throw new NotImplementedException();
        }


        private static Transaccion CreateReembolso(CsvRow row)
        {
        throw new NotImplementedException();
        }


        private static Transaccion CreateSoftStaking(CsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Compra = row.Instrument,
                Cantidad_Compra = row.TransactionQuantity,
                Detalles = "",
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "ingreso",
                Subtipo = "soft_staking_reward"
            };
            return transaccion;
        }


        private static Transaccion CreateSpotTrade(CsvRow row)
        {
            throw new NotImplementedException();
        }
    }
}
