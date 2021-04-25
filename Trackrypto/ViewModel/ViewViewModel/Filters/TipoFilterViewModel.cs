using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.ViewModel.Messenger;

namespace Trackrypto.ViewModel.ViewViewModel.Filters
{
    public class TipoFilterViewModel : ViewModelBase
    {
        #region private
        private bool showDepositos = true;
        private bool showIngresos = true;
        private bool showOperaciones = true ;
        private bool showPerdidas = true;
        private bool showRetiradas = true;
        #endregion

        #region properties
        public bool? ShowAll
        {
            get
            {
                if (showDepositos && showIngresos && showOperaciones && showPerdidas && showRetiradas)
                    return true;

                if (!showDepositos && !showIngresos && !showOperaciones && !showPerdidas && !showRetiradas)
                    return false;

                return null;
            }

            set
            {
                showDepositos = value ?? true;
                showIngresos = value ?? true;
                showOperaciones = value ?? true;
                showPerdidas = value ?? true;
                showRetiradas = value ?? true;

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowDepositos));
                RaisePropertyChanged(nameof(ShowIngresos));
                RaisePropertyChanged(nameof(ShowOperaciones));
                RaisePropertyChanged(nameof(ShowPerdidas));
                RaisePropertyChanged(nameof(ShowRetiradas));

                Update();
            }
        }

        public bool ShowDepositos
        {
            get => showDepositos;
            set
            {
                showDepositos = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowAll));
                Update();
            }
        }

        public bool ShowIngresos
        {
            get => showIngresos;
            set
            {
                showIngresos = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowAll));
                Update();
            }
        }

        public bool ShowOperaciones
        {
            get => showOperaciones;
            set
            {
                showOperaciones = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowAll));
                Update();
            }
        }

        public bool ShowPerdidas
        {
            get => showPerdidas;
            set
            {
                showPerdidas = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowAll));
                Update();
            }
        }

        public bool ShowRetiradas
        {
            get => showRetiradas;
            set
            {
                showRetiradas = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowAll));
                Update();
            }
        }
        #endregion


        private void Update()
        {
            var msg = new UpdateMessage() { Restore = true };
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(msg);
        }

        public string[] GetFilter()
        {
            var response = new List<string>();
            if (ShowDepositos) response.Add("deposito");
            if (ShowIngresos) response.Add("ingreso");
            if (ShowOperaciones) response.Add("operacion");
            if (ShowPerdidas) response.Add("perdida");
            if (ShowRetiradas) response.Add("retirada");
            return response.ToArray();
        }
    }
}
