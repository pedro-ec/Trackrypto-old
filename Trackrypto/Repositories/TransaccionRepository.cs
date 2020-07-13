using Dapper;
using Dapper.Contrib.Extensions;
using HMY.Infrastructure.AsyncResponse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Repositories
{
    public static class TransaccionRepository
    {
        static IDbConnection db = new SqlConnection("Server=localhost;Database=trackrypto;Uid=trackrypto_user;Pwd=trackrypto_password;");
        
        public static IResponse<Transaccion[]> GetTransacciones()
        {
            db.Open();
            var result = db.GetAll<Transaccion>().ToArray();
            db.Close();

            //string query = "SELECT * FROM transacciones";
            //var results = db.Query<Transaccion>(query).ToArray();

            return Response<Transaccion[]>.Ok(result);
        }

        public static IResponse<Transaccion> InsertTransaccion(Transaccion transaccion)
        {
            db.Open();
            var id = db.Insert(transaccion);
            db.Close();

            transaccion.Id = (int)id;
            return Response<Transaccion>.Ok(transaccion);
        }

        public static IResponse EditTransaccion(Transaccion transaccion)
        {
            db.Open();
            var updated = db.Update(transaccion);
            db.Close();

            if (updated == false) return Response.Error();
            return Response.Ok();
        }


        public static IResponse DeleteTransaccion(Transaccion transaccion)
        {
            db.Open();
            var updated = db.Delete(transaccion);
            db.Close();

            if (updated == false) return Response.Error();
            return Response.Ok();
        }
    }
}
