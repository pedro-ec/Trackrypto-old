using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Entities.Summary;
using Trackrypto.Model.Factories;

namespace Trackrypto.Repositories
{
    public static class SummaryRepository
    {
        #region private
        private static Domain model => Domain.GetModel();
        private static List<Transaccion> transacciones => model.transacciones;
        #endregion

        public static ExchangeSummary[] GetSummary() => SummaryFactory.GetSummary(transacciones.ToArray());
    }
}
