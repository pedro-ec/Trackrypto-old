using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackrypto.ViewModel.EntityViewModel
{
    public class TransaccionViewModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Subtipo { get; set; }
        public string Exchange { get; set; }
        public float? Cantidad_Compra { get; set; }
        public string Divisa_Compra { get; set; }
        public float? Cantidad_Venta { get; set; }
        public string Divisa_Venta { get; set; }
        public float? Cantidad_Comision { get; set; }
        public string Divisa_Comision { get; set; }
        public string Comentarios { get; set; }
        public DateTime Fecha { get; set; }
    }
}
