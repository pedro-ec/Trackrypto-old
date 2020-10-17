using HMY.Infrastructure.AsyncResponse;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Helpers
{
    public static class TransactionsFileManager
    {
        public static IResponse<Transaccion[]> GetTransacciones(string path)
        {
            if (!File.Exists(path)) return Response<Transaccion[]>.NotFound();

            string json = File.ReadAllText(path);

            try
            {
                Transaccion[] transacciones = JsonConvert.DeserializeObject<Transaccion[]>(json);
                return Response<Transaccion[]>.Ok(transacciones);
            }
            catch (Exception) 
            { 
                return Response<Transaccion[]>.Error(); 
            }
        }

        public static IResponse SaveTransacciones(Transaccion[] transacciones, string path)
        {
            string json = JsonConvert.SerializeObject(transacciones, 
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            File.WriteAllText(path, json);

            return Response.Ok();
        }
    }
}
