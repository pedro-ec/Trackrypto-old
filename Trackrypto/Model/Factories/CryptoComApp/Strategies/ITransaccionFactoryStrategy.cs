using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model.Factories.CryptoComApp.Strategies
{
    interface ITransaccionFactoryStrategy
    {
        public Transaccion GetTransaccion(object data);
    }
}
