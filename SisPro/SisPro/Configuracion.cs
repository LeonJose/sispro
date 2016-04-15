using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SisPro
{
    public class Configuracion : Conexion
    {
        #region Atributos

        private int _id;
        private string _impresora;
        private int _espera;

        #endregion 

        #region Propiedades

        public int  Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Impresora
        {
            get { return _impresora; }
            set { _impresora = value; }
        }


        public int Espera
        {
            get { return _espera; }
            set { _espera = value; }
        }

        #endregion

        #region Constructor

        public Configuracion()
        {
            _id = 0;
            _impresora = "";
            _espera = 0;
        }

        public Configuracion(int id)
        {
            string instruccion = "select conf_impresora, conf_espera from configuracion where conf_id=" + id;
            DataRow registro = LeerRegistro(instruccion);
            if (registro != null)
            {
                _id = id;
                _impresora = registro["conf_impresora"].ToString();
                _espera = int.Parse(registro["conf_espera"].ToString());
            }
            else
            {
                _id = 0;
                _impresora = "";
                _espera = 0;
            }
        }

        #endregion

        #region Metodos

        public bool AgregarConfiguracion()
        {
            string instruccion = "insert into configuracion(conf_impresora, conf_espera)values(@impresora,@caja,@espera)";
            SqlCommand comandosql = new SqlCommand(instruccion);
            comandosql.Parameters.Add(new SqlParameter("@impresora", _impresora));
            comandosql.Parameters.Add(new SqlParameter("@espera", _espera));
            return EjecutarComando(comandosql);
        }

        public bool EditarConfiguracion()
        {
            string instruccion = "update configuracion set conf_impresora=@impresora, conf_espera=@espera where conf_id=@id";
            SqlCommand comandosql = new SqlCommand(instruccion);
            comandosql.Parameters.Add(new SqlParameter("@impresora", _impresora));
            comandosql.Parameters.Add(new SqlParameter("@espera", _espera));
            comandosql.Parameters.Add(new SqlParameter("@id", _id));
            return EjecutarComando(comandosql);
        }

        #endregion
    }
}
