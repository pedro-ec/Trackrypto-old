using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;
using Trackrypto.ViewModel.EntityViewModel;

namespace Trackrypto.ViewModel.Factories
{
    public static class TransaccionFactory
    {
        public static TransaccionViewModel CreateTransaccion(Transaccion transaccion)
        {
            TransaccionViewModel transaccionViewModel = transaccion.Adapt<TransaccionViewModel>();
            return transaccionViewModel;
        }
    }
}
