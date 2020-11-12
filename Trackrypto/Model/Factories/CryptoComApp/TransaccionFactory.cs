using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Factories.CryptoComApp.Utils;

namespace Trackrypto.Model.Factories.CryptoComApp
{
    class KindConverter
    {
        public string Tipo { get; }
        public bool Alerta { get; }
        public string Mensaje_Alerta { get; }
    }
    
    public static class CryptoComTransaccionFactory
    {
        private static readonly string[] depositos =
        {
            "exchange_to_crypto_transfer"
        };

        private static readonly string[] ingresos = 
            {
            "referral_gift",
            "crypto_earn_interest_paid",
            "transfer_cashback",
            "dust_conversion_credited",
            "staking_reward",
            "referral_card_cashback",
            "reimbursement",
            "dynamic_coin_swap_bonus_exchange_deposit"
        };

        private static readonly string[] operaciones =
        {
            "crypto_purchase",
            "crypto_exchange",
            "crypto_viban_exchange",
            "viban_purchase",
            "card_top_up"
        };

        private static readonly string[] perdidas =
        {
            "dust_conversion_debited",
            "dynamic_coin_swap_debited"
        };

        private static readonly string[] retiradas =
        {
            "crypto_to_exchange_transfer",
            "crypto_withdrawal"
        };

        private static readonly string[] otros =
        {
            "crypto_transfer"
        };


        public static Transaccion FromAppCsv(string line)
        {
            //var values = line.Split(';');
            CsvRow row = CsvRow.FromCsv(line);

            if (depositos.Contains(row.TransactionKind)) return CreateDeposito(row);
            if (ingresos.Contains(row.TransactionKind)) return CreateIngreso(row);
            if (operaciones.Contains(row.TransactionKind)) return CreateOperacion(row);
            if (perdidas.Contains(row.TransactionKind)) return CreatePerdida(row);
            if (retiradas.Contains(row.TransactionKind)) return CreateRetirada(row);
            if (otros.Contains(row.TransactionKind)) return CreateOtro(row);

            return null;
        }

        private static Transaccion CreateDeposito(CsvRow row)
        {
            Transaccion transaccion = DefaultTransaccion(row);

            transaccion.Tipo = "deposito";

            return transaccion;
        }

        private static Transaccion CreateIngreso(CsvRow row)
        {
            Transaccion transaccion = DefaultTransaccion(row);

            transaccion.Tipo = "ingreso";

            return transaccion;
        }

        private static Transaccion CreateOperacion(CsvRow row)
        {
            Transaccion transaccion = DefaultTransaccion(row);

            transaccion.Tipo = "operacion";

            return transaccion;
        }

        private static Transaccion CreatePerdida(CsvRow row)
        {
            Transaccion transaccion = DefaultTransaccion(row);

            transaccion.Tipo = "perdida";

            return transaccion;
        }

        private static Transaccion CreateRetirada(CsvRow row)
        {
            Transaccion transaccion = DefaultTransaccion(row);

            transaccion.Tipo = "retirada";

            if (string.Equals(row.TransactionKind, "crypto_withdrawal"))
            {
                transaccion.Alerta = true;
                transaccion.Mensaje_Alerta = "Revisar comisión";
            }

            return transaccion;
        }

        private static Transaccion CreateOtro(CsvRow row)
        {
            Transaccion transaccion = DefaultTransaccion(row);


            if (string.Equals(row.TransactionKind, "crypto_transfer"))
            {
                if (row.Amount < 0) transaccion.Tipo = "retirada";
                else transaccion.Tipo = "deposito";

                transaccion.Alerta = true;
                transaccion.Mensaje_Alerta = "Comprobar";
            }


            return transaccion;
        }

        private static Transaccion DefaultTransaccion(CsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_app",
                Comentarios = row.TransactionDescription,
                Detalles = row.TransactionKind,
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = ""
            };

            if (row.NativeAmount != null)
                if (string.Equals(row.NativeCurrency, "EUR"))
                    transaccion.Valor_Eur = row.NativeAmount;

            if (row.Amount > 0)
            {
                transaccion.Cantidad_Compra = row.Amount;
                transaccion.Divisa_Compra = row.Currency;
            }

            if (row.Amount < 0)
            {
                transaccion.Cantidad_Venta = -row.Amount;
                transaccion.Divisa_Venta = row.Currency;
                if (!string.IsNullOrWhiteSpace(row.ToCurrency))
                {
                    transaccion.Cantidad_Compra = row.ToAmount;
                    transaccion.Divisa_Compra = row.ToCurrency;
                }
            }

            return transaccion;
        }
    }
}
