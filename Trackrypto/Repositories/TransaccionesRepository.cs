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
        private static Domain model => Domain.GetModel();
        private static List<Transaccion> transacciones => model.transacciones;
        #endregion


        #region transacciones
        public static Transaccion[] GetTransacciones() =>
            transacciones.ToArray();

        public static Transaccion[] GetTransacciones(TransaccionesFilter filter) =>
            transacciones
                .Where(transaccion => filter.Tipo?.Contains(transaccion.Tipo) ?? true)
                .Where(transaccion => filter.Subtipo?.Contains(transaccion.Subtipo) ?? true)
                .Where(transaccion => filter.Exchange?.Contains(transaccion.Exchange) ?? true)
                .Where(transaccion =>
                    filter.Simbolo.Contains(transaccion.Divisa_Compra)
                    || filter.Simbolo.Contains(transaccion.Divisa_Venta)
                    || filter.Simbolo.Contains(transaccion.Divisa_Comision))
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


        #region subtipos
        public static string[] GetTipos() =>
            transacciones
            .Select(transaccion => transaccion.Tipo)
            .Distinct()
            .OrderBy(value => value)
            .ToArray();
        #endregion

        #region subtipos
        public static string[] GetSubtipos() =>
            transacciones
            .Select(transaccion => transaccion.Subtipo)
            .Distinct()
            .OrderBy(value => value)
            .ToArray();
        #endregion

        #region exchanges
        public static string[] GetExchanges() =>
            transacciones
            .Select(transaccion => transaccion.Exchange)
            .Distinct()
            .OrderBy(value => value)
            .ToArray();
        #endregion

        #region Symbols
        public static string[] GetSymbols() =>
            transacciones
            .Select(transaccion => transaccion.Divisa_Compra)
            .Concat(
                transacciones
                .Select(transaccion => transaccion.Divisa_Venta))
            .Concat(
                transacciones
                .Select(transaccion => transaccion.Divisa_Comision))
            .Distinct()
            .OrderBy(value => value)
            .ToArray();
        #endregion
    }
}
