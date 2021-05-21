using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackrypto.Model;
using Trackrypto.Repositories;
using Trackrypto.ViewModel.Messenger;

namespace Trackrypto.ViewModel.EntityViewModel
{
    public class TransaccionViewModel
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Subtipo { get; set; }
        public string Exchange { get; set; }
        public float? Cantidad_Compra { get; set; }
        public string Divisa_Compra { get; set; }
        public float? Cantidad_Venta { get; set; }
        public string Divisa_Venta { get; set; }
        public decimal? Valor_Eur { get; set; }
        public float? Cantidad_Comision { get; set; }
        public string Divisa_Comision { get; set; }
        public string Comentarios { get; set; }
        public string Detalles { get; set; }
        public DateTime Fecha { get; set; }
        public bool Alerta { get; set; }
        public string Mensaje_Alerta { get; set; }


        public TransaccionViewModel()
        {
            WireCommands();
        }


        #region commands
        public ICommand DeleteCommand { get; private set; }
        private void WireCommands()
        {
            DeleteCommand = new RelayCommand(() => Delete());
        }
        #endregion


        private void Delete()
        {
            TransaccionesRepository.DeleteTransaccion(this.Id);
            var msg = new UpdateMessage() { Restore = false };
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(msg);
        }
    }

}
