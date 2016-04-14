using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SisPro
{
    class Configuracion : Conexion
    {
        #region Atributos

        private int _id;
        private string _impresora;
        private Caja _caja;
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

        public Caja Caja
        {
            get { return _caja; }
            set { _caja = value; }
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
            _caja = new Caja();
            _espera = 0;
        }

        public Configuracion(int id)
        {
            string instruccion = "select conf_impresora, conf_caja_id, conf_espera from configuraciones where conf_id=" + id;
            DataRow registro = LeerRegistro(instruccion);
            if (registro != null)
            {
                _id = id;
                _impresora = registro["conf_impresora"].ToString();
                _caja = new Caja(int.Parse(registro["conf_caja_id"].ToString()));
                _espera = int.Parse(registro["conf_espera"].ToString());
            }
            else
            {
                _id = 0;
                _impresora = "";
                _caja = new Caja();
                _espera = 0;
            }
        }

        #endregion

        #region Metodos

        public bool AgregarConfiguracion()
        {
            string instruccion = "insert into configuracion(conf_impresora, conf_caja_id, conf_espera)values(@impresora,@caja,@espera)";
            SqlCommand comandosql = new SqlCommand(instruccion);
            comandosql.Parameters.Add(new SqlParameter("@impresora", _impresora));
            comandosql.Parameters.Add(new SqlParameter("@caja", _caja.Id));
            comandosql.Parameters.Add(new SqlParameter("@espera", _espera));
            return EjecutarComando(comandosql);
        }

        public bool EditarConfiguracion()
        {
            string instruccion = "update configuracion set conf_impresora=@impresora, conf_caja_id=@caja, conf_espera=@espera where conf_id=@id";
            SqlCommand comandosql = new SqlCommand(instruccion);
            comandosql.Parameters.Add(new SqlParameter("@impresora", _impresora));
            comandosql.Parameters.Add(new SqlParameter("@caja", _caja.Id));
            comandosql.Parameters.Add(new SqlParameter("@espera", _espera));
            comandosql.Parameters.Add(new SqlParameter("@id", _id));
            return EjecutarComando(comandosql);
        }

        #endregion
    }
}
