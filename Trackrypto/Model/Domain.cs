using HMY.Infrastructure.AsyncResponse;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Helpers;
using Trackrypto.Model.Entities;
using Trackrypto.Model.Entities.Summary;

namespace Trackrypto.Model
{
    public class Domain
    {
        public List<Transaccion> transacciones { get; }

        #region singleton
        private Domain()
        {
            transacciones = new List<Transaccion>();
        }

        private static Domain model;

        public static Domain GetModel()
        {
            if (model == null) model = new Domain();
            return model;
        }
        #endregion

        public ExchangeSummary[] GetSummary()
        {
            var exchanges = new List<ExchangeSummary>();

            foreach (Transaccion transaccion in transacciones)
            {

            }

            return exchanges.ToArray();
        }

        public void OpenFile(string path)
        {
            var response = TransactionsFileManager.GetTransacciones(path);
            if (response.Type != ResponseType.Ok)
            {
                // error
                return;
            }

            var newTransacciones = response.Data;
            transacciones.Clear();
            transacciones.AddRange(newTransacciones);
        }

        public void Save(string path) =>
            TransactionsFileManager.SaveTransacciones(transacciones.ToArray(), path);
    }
}
