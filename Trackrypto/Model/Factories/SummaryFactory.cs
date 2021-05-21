using System.Collections.Generic;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Entities.Summary;
using System.Linq;

namespace Trackrypto.Model.Factories
{
    public static class SummaryFactory
    {
        public static ExchangeSummary[] GetSummary(Transaccion[] transacciones)
        {
            var exchanges = new List<ExchangeSummary>();

            foreach (Transaccion transaccion in transacciones)
            {
                ExchangeSummary exchange = GetExchange(exchanges, transaccion);
                Calculate(exchange, transaccion);
            }

            return exchanges.ToArray();
        }

        private static ExchangeSummary GetExchange(List<ExchangeSummary> exchanges, Transaccion transaccion)
        {
            ExchangeSummary exchange = exchanges.FirstOrDefault(exchange => exchange.Name == transaccion.Exchange);
            if (exchange == null)
            {
                exchange = new ExchangeSummary(transaccion.Exchange);
                exchanges.Add(exchange);
            }
            return exchange;
        }

        private static void Calculate(ExchangeSummary exchange, Transaccion transaccion)
        {
            if (!string.IsNullOrWhiteSpace(transaccion.Divisa_Compra))
            {
                var coin = GetCoin(exchange.Coins, transaccion.Divisa_Compra);
                coin.Value += transaccion.Cantidad_Compra ?? 0;
            }

            if (!string.IsNullOrWhiteSpace(transaccion.Divisa_Venta))
            {
                var coin = GetCoin(exchange.Coins, transaccion.Divisa_Venta);
                coin.Value -= transaccion.Cantidad_Venta ?? 0;
            }

            if (!string.IsNullOrWhiteSpace(transaccion.Divisa_Comision))
            {
                var coin = GetCoin(exchange.Coins, transaccion.Divisa_Comision);
                coin.Value -= transaccion.Cantidad_Comision ?? 0;
            }
        }

        private static CoinSummary GetCoin(List<CoinSummary> coins, string symbol)
        {
            CoinSummary coin = coins.FirstOrDefault(coin => coin.Symbol == symbol);
            if (coin == null)
            {
                coin = new CoinSummary(symbol);
                coins.Add(coin);
            }
            return coin;
        }
    }
}
