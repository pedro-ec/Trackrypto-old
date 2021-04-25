using HMY.Infrastructure.AsyncResponse;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Helpers;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model
{
    public class Domain
    {
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

        private List<Transaccion> transacciones;

        public Transaccion[] GetTransacciones()
        {
            return transacciones.ToArray();
        }

        public Transaccion[] GetTransacciones(TransaccionesFilter filter)
        {
            return transacciones
                .Where(transaccion => filter.Tipo.Contains(transaccion.Tipo))
                .ToArray();
        }

        public void InsertTransaccion(Transaccion transaccion)
        {
            transacciones.Add(transaccion);
        }

        public void InsertTransacciones(Transaccion[] newTransacciones)
        {
            transacciones.AddRange(newTransacciones);
        }

        public void DeleteTransaccion(Guid id)
        {
            transacciones.Remove(transacciones.FirstOrDefault(x => x.Id == id));
        }

        public void DeleteTransacciones(Guid[] ids)
        {
            transacciones.RemoveAll(x => ids.Contains(x.Id));
        }

        public void Save(string path)
        {
            TransactionsFileManager.SaveTransacciones(transacciones.ToArray(), path);
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
    }
}
