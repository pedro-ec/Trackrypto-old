using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackrypto.Model.Entities;

namespace Trackrypto.Model
{
    public class Model
    {
        #region singleton
        private Model()
        {
            Transacciones = new List<Transaccion>();
        }

        private static Model model;

        public static Model GetModel()
        {
            if (model == null) model = new Model();
            return model;
        }
        #endregion

        public List<Transaccion> Transacciones { get; }



    }
}
