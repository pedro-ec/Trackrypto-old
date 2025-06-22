using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Ubiety.Dns.Core.Records;

namespace Trackrypto.Model.Factories.CryptoComApp
{
    public static class TransaccionFactory
    {
        private static readonly string[] depositos =
        {
            "exchange_to_crypto_transfer",
            "crypto_deposit"
        };

        private static readonly string[] ingresos = 
            {
            "referral_gift",
            "referral_bonus",
            "crypto_earn_interest_paid",
            "transfer_cashback",
            "dust_conversion_credited",
            "staking_reward",
            "mco_stake_reward",
            "finance.lockup.dpos_compound_interest.crypto_wallet",
            "referral_card_cashback",
            "reimbursement",
            "admin_wallet_credited",
            "dynamic_coin_swap_credited",
            "dynamic_coin_swap_bonus_exchange_deposit",
            "reward.loyalty_program.trading_rebate.crypto_wallet"
        };

        private static readonly string[] operaciones =
        {
            "crypto_purchase",
            "crypto_exchange",
            "crypto_viban_exchange",
            "viban_purchase",
            "card_top_up",
            "crypto_payment",
            "crypto_payment_refund",
            "nft_payout_credited",
            "trading.crypto_purchase.twap.cash_account",
            "trading.crypto_sale.twap.cash_account",
            "trading.defi_purchase.cash_account"
        };

        private static readonly string[] perdidas =
        {
            "dust_conversion_debited",
            "dynamic_coin_swap_debited",
            "admin_wallet_debited",
            "card_cashback_reverted"
        };

        private static readonly string[] retiradas =
        {
            "crypto_to_exchange_transfer",
            "crypto_withdrawal"
        };

        private static readonly string[] otros =
        {
            "crypto_transfer",
            "red_envelope_transfer"
        };

        private static readonly string[] ignore =
        {
            "crypto_earn_program_created",
            "crypto_earn_program_withdrawn",
            "supercharger_deposit",
            "supercharger_withdrawal",
            "lockup_swap_debited",
            "lockup_swap_credited",
            "crypto_wallet_swap_credited",
            "crypto_wallet_swap_debited",
            "lockup_swap_rebate",
            "lockup_lock",
            "lockup_unlock",
            "lockup_upgrade",
            "trading.limit_order.cash_account.sell_lock",
            "trading.limit_order.cash_account.sell_unlock",
            "finance.lockup.dpos_lock_upgrade.crypto_wallet",
            "finance.lockup.dpos_lock.crypto_wallet"
        };

        public static Transaccion GetTransaccion(string line)
        {
            //var values = line.Split(';');
            CsvRow row = CsvRow.FromCsv(line);

            if (depositos.Contains(row.TransactionKind)) return CreateDeposito(row);
            if (ingresos.Contains(row.TransactionKind)) return CreateIngreso(row);
            if (operaciones.Contains(row.TransactionKind)) return CreateOperacion(row);
            if (perdidas.Contains(row.TransactionKind)) return CreatePerdida(row);
            if (retiradas.Contains(row.TransactionKind)) return CreateRetirada(row);
            if (otros.Contains(row.TransactionKind)) return CreateOtro(row);
            if (ignore.Contains(row.TransactionKind)) return null;

            return CreateDefault(row, line);
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

            if (string.Equals(row.TransactionKind, "nft_payout_credited"))
            {
                transaccion.Alerta = true;
                transaccion.Mensaje_Alerta = "Añadir compra de los NFTs";
            }

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

        private static Transaccion CreateDefault(CsvRow row, string line)
        {
            Transaccion transaccion = DefaultTransaccion(row);

            transaccion.Alerta = true;
            transaccion.Mensaje_Alerta = line;
            


            return transaccion;
        }

        private static Transaccion DefaultTransaccion(CsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_app",
                Detalles = row.TransactionDescription,
                Subtipo = row.TransactionKind,
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = ""
            };

            if (row.NativeAmount != null)
            {
                if (string.Equals(row.NativeCurrency, "EUR"))
                    transaccion.Valor_Eur = row.NativeAmount;
                else
                {
                    transaccion.Alerta = true;
                    if (!string.IsNullOrWhiteSpace(transaccion.Mensaje_Alerta))
                        transaccion.Mensaje_Alerta += "\n";
                    transaccion.Mensaje_Alerta += ("Native Amount: " + row.NativeAmount + " " + row.NativeCurrency);
                }
            }

            if (row.Amount > 0)
            {
                transaccion.Cantidad_Compra = row.Amount;
                transaccion.Divisa_Compra = row.Currency.Trim(' ');
                return transaccion;
            }

            if (row.Amount < 0)
            {
                transaccion.Cantidad_Venta = -row.Amount;
                transaccion.Divisa_Venta = row.Currency.Trim(' ');
                if (!string.IsNullOrWhiteSpace(row.ToCurrency))
                {
                    transaccion.Cantidad_Compra = row.ToAmount;
                    transaccion.Divisa_Compra = row.ToCurrency.Trim(' ');
                }
                return transaccion;
            }

            transaccion.Alerta = true;
            if (!string.IsNullOrWhiteSpace(transaccion.Mensaje_Alerta))
                transaccion.Mensaje_Alerta += "\n";
            transaccion.Mensaje_Alerta += "Empty Amount";
            return transaccion;
        }
    }
}
