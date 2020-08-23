using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.ViewModel.EntityViewModel;

namespace Trackrypto.ViewModel.ViewViewModel
{
    public class EditTransaccionDialogViewModel : ViewModelBase
    {
        public string DialogTitle { get; }
        public TransaccionViewModel Transaccion { get; }


        #region constructor
        public EditTransaccionDialogViewModel(TransaccionViewModel transaccion = null)
        {
            if(transaccion == null)
            {
                DialogTitle = "Nueva Transacción";
                Transaccion = new TransaccionViewModel { Fecha = DateTime.Now };
            }
            else
            {
                DialogTitle = "Editar Transacción";
                Transaccion = transaccion;
            }
        }
        #endregion

        //#region commands
        //private void WireCommands()
        //{

        //}
        //#endregion
    }
}
