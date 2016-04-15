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

        private int _id;
        private string _nombre;

        #endregion

        #region Propiedades

        public int IdDepto
        {
            get { return _id; }
            set { _id = value; }
        }
        public string NombreDepto
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        #endregion

        #region Construcctor

        public Departamento()
        {
            _id= 0;
            _nombre="";
        }

        #endregion

        #region Metodos

        public Departamento(int idepto)
        {
            string consulta = "select dep_nombre from departamentos where dep_id="+idepto;
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                _id = idepto;
                _nombre = registro["dep_nombre"].ToString();
            }
            else
            {
                _id = 0;
                _nombre = "";
            }
        }

        public override string ToString()
        {
            return _nombre.ToString();
        }

        #endregion
    }
}
