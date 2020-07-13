using System;

namespace Trackrypto.Model.Entities
{
    public class Transaccion
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Origen { get; set; }
        public float Origen_Valor { get; set; }
        public string Origen_Divisa { get; set; }
        public string Destino { get; set; }
        public float Destino_Valor { get; set; }
        public string Destino_Divisa { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; }
    }
}
