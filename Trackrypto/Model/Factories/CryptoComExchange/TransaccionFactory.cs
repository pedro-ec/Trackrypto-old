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
        public static Transaccion GetDeposito(string line)
        {
            var row = DepositosRetirosCsvRow.FromCsv(line);
            return CreateDeposito(row);
        }

        public static Transaccion GetRetirada(string line)
        {
            var row = DepositosRetirosCsvRow.FromCsv(line);
            return CreateRetirada(row);
        }

        public static Transaccion GetStake(string line)
        {
            var row = StakeCsvRow.FromCsv(line);
            return CreateStake(row);
        }

        public static Transaccion GetReembolso(string line)
        {
            var row = ReembolsosCsvRow.FromCsv(line);
            return CreateReembolso(row);
        }

        public static Transaccion GetSoftStaking(string line)
        {
            var row = SoftStakingCsvRow.FromCsv(line);
            return CreateSoftStaking(row);
        }

        public static Transaccion GetSpotTrade(string line)
        {
            var row = SpotWalletCsvRow.FromCsv(line);
            return CreateSpotTrade(row);
        }


        private static Transaccion CreateDeposito(DepositosRetirosCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Compra = row.Currency,
                Cantidad_Compra = row.Amount,
                Divisa_Comision = row.Currency,
                Cantidad_Comision = row.Fee,
                Detalles = row.Address,
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "deposito"
            };
            return transaccion;
        }


        private static Transaccion CreateRetirada(DepositosRetirosCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Venta = row.Currency,
                Cantidad_Venta = row.Amount,
                Divisa_Comision = row.Currency,
                Cantidad_Comision = row.Fee,
                Detalles = row.Address,
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "retirada"
            };
            return transaccion;
        }


        private static Transaccion CreateStake(StakeCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Compra = row.InterestCurrency,
                Cantidad_Compra = row.InterestAmount,
                Detalles = string.Concat("STAKE: ", row.StakeAmount, " ", row.StakeCurrency),
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "ingreso",
                Subtipo = "stake_reward"
            };
            return transaccion;
        }


        private static Transaccion CreateReembolso(ReembolsosCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Compra = row.RebateCurrency,
                Cantidad_Compra = row.RebateAmount,
                Detalles = string.Concat(
                    row.RebatePercentage, 
                    " of " , row.TradeFeeAmount, " ", row.TradeFeeCurrency, 
                    " to ", row.RebateDestination),
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "ingreso",
                Subtipo = "fee_rebate"
            };
            return transaccion;
        }


        private static Transaccion CreateSoftStaking(SoftStakingCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Compra = row.InterestCurrency,
                Cantidad_Compra = row.InterestAmount,
                Detalles = string.Concat(
                    "STAKE: ", row.StakeAmount, " ", row.StakeCurrency, ", ", 
                    row.Apr, " APR"),
                Fecha = row.TimestampUTC,
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "ingreso",
                Subtipo = "soft_staking_reward"
            };
            return transaccion;
        }


        private static Transaccion CreateSpotTrade(SpotWalletCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Exchange = "crypto.com_exchange",
                Divisa_Comision = row.FeeCurrency,
                Cantidad_Comision = row.Fee,
                Fecha = row.TimestampUTC,
                Detalles = row.TradeId.ToString(),
                Alerta = false,
                Mensaje_Alerta = "",
                Tipo = "operacion"
            };

            var divisas = row.Symbol.Split('_');

            if (string.Equals(row.Side, "BUY"))
            {
                transaccion.Divisa_Compra = divisas[0];
                transaccion.Cantidad_Compra = row.TradedQuantity;
                transaccion.Divisa_Venta = divisas[1];
                transaccion.Cantidad_Venta = row.TradedQuantity * row.TradedPrice;
            }

            if (string.Equals(row.Side, "SELL"))
            {
                transaccion.Divisa_Venta = divisas[0];
                transaccion.Cantidad_Venta = row.TradedQuantity;
                transaccion.Divisa_Compra = divisas[1];
                transaccion.Cantidad_Compra = row.TradedQuantity * row.TradedPrice;
            }

            return transaccion;
        }
    }
}
