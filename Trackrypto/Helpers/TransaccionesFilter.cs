using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.Helpers
{
    public class TransaccionesFilter
    {
        public string[] Tipo { get; set; }
        public string[] Subtipo { get; }
        public string[] Exchange { get; set; }
        public decimal? Cantidad_Compra_Min { get; }
        public decimal? Cantidad_Compra_Max { get; }
        public string[] Divisa_Compra { get; }
        public decimal? Cantidad_Venta_Min { get; }
        public decimal? Cantidad_Venta_Max { get; }
        public string[] Divisa_Venta { get; }
        public decimal? Valor_Eur_Min { get; }
        public decimal? Valor_Eur_Max { get; }
        public decimal? Cantidad_Comision_Min { get; }
        public decimal? Cantidad_Comision_Max { get; }
        public string[] Divisa_Comision { get; }
        public string[] Comentarios { get; }
        public string[] Detalles { get; }
        public DateTime Fecha_Min { get; }
        public DateTime Fecha_Max { get; }
        public bool? Alerta { get; }
        public string[] Mensaje_Alerta { get; }
    }
}
