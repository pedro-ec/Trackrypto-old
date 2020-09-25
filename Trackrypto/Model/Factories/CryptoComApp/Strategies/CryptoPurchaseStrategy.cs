using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Factories.CryptoComApp.Strategies;

namespace Trackrypto.Model.Factories.Strategies.CryptoComApp
{
    class CryptoPurchaseStrategy : BaseStrategy
    {
        public override Transaccion GetTransaccion(object data)
        {
            Transaccion transaccion = base.GetTransaccion(data);
            transaccion.Tipo = "operacion";
            return transaccion;
        }
    }
}
