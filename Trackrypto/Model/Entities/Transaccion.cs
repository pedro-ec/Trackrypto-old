using System;

namespace Trackrypto.Model.Entities
{
    public class Transaccion
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Subtipo { get; set; }
        public string Exchange { get; set; }
        public decimal? Cantidad_Compra { get; set; }
        public string Divisa_Compra { get; set; }
        public decimal? Cantidad_Venta { get; set; }
        public string Divisa_Venta { get; set; }
        public decimal? Valor_Eur { get; set; }
        public decimal? Cantidad_Comision { get; set; }
        public string Divisa_Comision { get; set; }
        public string Comentarios { get; set; }
        public string Detalles { get; set; }
        public DateTime Fecha { get; set; }
        public bool Alerta { get; set; }
        public string Mensaje_Alerta { get; set; }
    }
}
