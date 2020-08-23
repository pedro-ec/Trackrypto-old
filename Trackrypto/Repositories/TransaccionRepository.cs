using Dapper;
using Dapper.Contrib.Extensions;
using HMY.Infrastructure.AsyncResponse;
using MySql.Data.MySqlClient;
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
        static IDbConnection db = new MySqlConnection("Server=localhost;Database=trackrypto;Uid=trackrypto_user;Pwd=trackrypto_password;");
        
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

        public static IResponse BulkInsertTransaccion(Transaccion[] transacciones)
        {
            var sql = @"
                insert into transaccion
                    (Id, Tipo, Subtipo, Exchange, Cantidad_Compra, Divisa_Compra, Cantidad_Venta, Divisa_Venta, Cantidad_Comision, Divisa_Comision, Comentarios, Fecha) 
                values 
                    (@Id, @Tipo, @Subtipo, @Exchange, @Cantidad_Compra, @Divisa_Compra, @Cantidad_Venta, @Divisa_Venta, @Cantidad_Comision, @Divisa_Comision, @Comentarios, @Fecha)";

            db.Open();
            var insertedRows = db.Execute(sql, transacciones);
            db.Close();

            if (insertedRows != transacciones.Length) return Response.Warning();
            return Response.Ok();
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
