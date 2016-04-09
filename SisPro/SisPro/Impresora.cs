using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SisPro
{
    class Impresora : Conexion
    {
        #region Atributos

        private int _idimpresora;
        private string _nombre;

        #endregion 

        #region Propiedades

        public int  IdImpresora
        {
            get { return _idimpresora; }
            set { _idimpresora = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        #endregion

        #region Constructor

        public Impresora()
        {
            _idimpresora = 0;
            _nombre = "";
        }

        public Impresora(int id)
        {
            string instruccion = "select nombre from impresora where Idimpresora=" + id;
            DataRow registro = LeerRegistro(instruccion);
            if (registro != null)
            {
                _idimpresora = id;
                _nombre = registro["nombre"].ToString();
            }
            else
            {
                _idimpresora = 0;
                _nombre = "";
            }
        }

        #endregion

        #region Metodos

        public bool AgregarImpresora()
        {
            string instruccion = "insert into impresora(nombre)values(@nom)";
            SqlCommand comandosql = new SqlCommand(instruccion);
            comandosql.Parameters.Add(new SqlParameter("@id", _idimpresora));
            comandosql.Parameters.Add(new SqlParameter("@nom", _nombre));
            return EjecutarComando(comandosql);
        }

        public bool EditarImpresora()
        {
            string instruccion = "update impresora set nombre=@nom where Idimpresora=@id";
            SqlCommand comandosql = new SqlCommand(instruccion);
            comandosql.Parameters.Add(new SqlParameter("@nom", _nombre));
            comandosql.Parameters.Add(new SqlParameter("@id",_idimpresora));
            return EjecutarComando(comandosql);jgfjhgfhjfjhkghgvjc
        }

        #endregion
    }
}
