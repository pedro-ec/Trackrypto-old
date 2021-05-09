using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model.Factories.Etherscan
{
    public static class TransaccionFactory
    {
        public static Transaccion GetEthereumTransaccion(string line)
        {
            var row = EthereumCsvRow.FromCsv(line);
            return CreateEthereumTransaccion(row);
        }

        public static Transaccion GetTokenTransaccion(string line, string key)
        {
            var row = TokenCsvRow.FromCsv(line);
            return CreateTokenTransaccion(row, key);
        }

        #region ethereum
        private static Transaccion CreateEthereumTransaccion(EthereumCsvRow row)
        {
            if (row.Value_IN_ETH != 0) return CreateEthereumDeposito(row);
            if (row.Value_OUT_ETH != 0) return CreateEthereumRetirada(row);
            if (row.TxnFeeETH != 0) return CreateEthereumComision(row);
            return null;
        }

        private static Transaccion CreateEthereumDeposito(EthereumCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "deposito",
                Exchange = row.To,
                Cantidad_Compra = row.Value_IN_ETH,
                Divisa_Compra = "ETH",
                Cantidad_Comision = row.TxnFeeETH,
                Divisa_Comision = "ETH",
                Detalles = row.From,
                Comentarios = "",
                Fecha = row.TimestampUTC,
                Alerta = !string.IsNullOrWhiteSpace(row.Status),
                Mensaje_Alerta = string.IsNullOrWhiteSpace(row.Status) ? "" : $"{row.Status}: {row.ErrCode}",
            };
            return transaccion;
        }

        private static Transaccion CreateEthereumRetirada(EthereumCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "retirada",
                Exchange = row.From,
                Cantidad_Venta = row.Value_OUT_ETH,
                Divisa_Venta = "ETH",
                Cantidad_Comision = row.TxnFeeETH,
                Divisa_Comision = "ETH",
                Detalles = row.To,
                Comentarios = "",
                Fecha = row.TimestampUTC,
                Alerta = !string.IsNullOrWhiteSpace(row.Status),
                Mensaje_Alerta = string.IsNullOrWhiteSpace(row.Status) ? "" : $"{row.Status}: {row.ErrCode}",
            };
            return transaccion;
        }

        private static Transaccion CreateEthereumComision(EthereumCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "perdida",
                Subtipo = "comisiones",
                Exchange = row.From,
                Cantidad_Comision = row.TxnFeeETH,
                Divisa_Comision = "ETH",
                Comentarios = row.To,
                Detalles = "",
                Fecha = row.TimestampUTC,
                Alerta = !string.IsNullOrWhiteSpace(row.Status),
                Mensaje_Alerta = string.IsNullOrWhiteSpace(row.Status) ? "" : $"{row.Status}: {row.ErrCode}",
            };
            return transaccion;
        }
        #endregion

        #region token
        private static Transaccion CreateTokenTransaccion(TokenCsvRow row, string key)
        {
            if (string.Equals(row.To, key)) return CreateTokenDeposito(row);
            if (string.Equals(row.From, key)) return CreateTokenRetirada(row);
            return null;
        }

        private static Transaccion CreateTokenDeposito(TokenCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "deposito",
                Exchange = row.To,
                Cantidad_Compra = row.Value,
                Divisa_Compra = row.TokenSymbol,
                Detalles = row.From,
                Comentarios = row.ContractAddress,
                Fecha = row.TimestampUTC
            };
            return transaccion;
        }

        private static Transaccion CreateTokenRetirada(TokenCsvRow row)
        {
            Transaccion transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = "retirada",
                Exchange = row.From,
                Cantidad_Venta = row.Value,
                Divisa_Venta = row.TokenSymbol,
                Detalles = row.To,
                Comentarios = row.ContractAddress,
                Fecha = row.TimestampUTC
            };
            return transaccion;
        }
        #endregion
    }
}
