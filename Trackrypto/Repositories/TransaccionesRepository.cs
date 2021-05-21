using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Helpers;
using Trackrypto.Model;
using Trackrypto.Model.Entities;

namespace Trackrypto.Repositories
{
    public static class TransaccionesRepository
    {
        #region private
        private static Domain model;
        private static List<Transaccion> transacciones => model.transacciones;
        #endregion


        #region transacciones
        public static Transaccion[] GetTransacciones() =>
            transacciones.ToArray();

        public static Transaccion[] GetTransacciones(TransaccionesFilter filter) =>
            transacciones
                .Where(transaccion => filter.Tipo.Contains(transaccion.Tipo))
                .ToArray();

        public static void InsertTransaccion(Transaccion transaccion) =>
            transacciones.Add(transaccion);

        public static void InsertTransacciones(Transaccion[] newTransacciones) =>
            transacciones.AddRange(newTransacciones);

        public static void DeleteTransaccion(Guid id) =>
            transacciones.Remove(transacciones.FirstOrDefault(x => x.Id == id));

        public static void DeleteTransacciones(Guid[] ids) =>
            transacciones.RemoveAll(x => ids.Contains(x.Id));
        #endregion


        #region exchanges
        public static string[] GetExchanges() =>
            transacciones
            .Select(transaccion => transaccion.Exchange)
            .Distinct()
            .ToArray();
        #endregion
    }
}
