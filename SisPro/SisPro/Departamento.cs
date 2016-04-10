using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SisPro
{
    public class Departamento : Conexion
    {
        #region Atributos

        private int _idDepto;
        private string _nombreDepto;

        #endregion

        #region Propiedades

        public int IdDepto
        {
            get { return _idDepto; }
            set { _idDepto = value; }
        }
        public string NombreDepto
        {
            get { return _nombreDepto; }
            set { _nombreDepto = value; }
        }

        #endregion

        #region Construcctor

        public Departamento()
        {
            _idDepto= 0;
            _nombreDepto="";
        }

        #endregion

        #region Metodos

        public Departamento(int idepto)
        {
            string consulta = "select dep_nombre from departamentos where dep_id="+idepto;
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                _idDepto = idepto;
                _nombreDepto = registro["dep_nombre"].ToString();
            }
            else
            {
                _idDepto = 0;
                _nombreDepto = "";
            }
        }

        public override string ToString()
        {
            return _nombreDepto.ToString();
        }

        #endregion
    }
}
