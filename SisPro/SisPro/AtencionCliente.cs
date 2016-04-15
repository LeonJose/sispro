using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace SisPro
{
    public class AtencionCliente : Conexion
    {
        #region Atributos

        private int _id;
        private DateTime _fecha;
        private DateTime _inicio;
        private DateTime _fin;
        private bool _esAlum;
        private Espera _espId;
        private Empleado _empId;
        private Caja _cajaId;

        #endregion

        #region Propiedades

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public DateTime Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }
        public DateTime Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }
        public bool EsAlumno
        {
            get { return _esAlum; }
            set { _esAlum = value; }
        }
        public Espera Espera
        {
            get { return _espId; }
            set { _espId = value; }
        }
        public Empleado EmpleadoId
        {
            get { return _empId; }
            set { _empId = value; }
        }
        public Caja CajaId
        {
            get { return _cajaId; }
            set { _cajaId = value; }
        }

        #endregion

        #region Constructor

        public AtencionCliente()
        {
            _id = 0;
            _fecha = new DateTime();
            _inicio = new DateTime();
            _fin = new DateTime();
            _esAlum = false;
            _espId = new Espera();
            _empId = new Empleado();
            _cajaId = new Caja();
        }

        #endregion

        #region Metodos

        public AtencionCliente(int i)
        {
            string consulta = "select ac_fecha, ac_inicio, ac_fin, ac_esAlumno, ac_esp_id, ac_emp_id, ac_caja_id from AtencionClientes where ac_id =" + i;
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                _id = i;
                _fecha = Convert.ToDateTime(registro["ac_fecha"].ToString());
                _inicio = Convert.ToDateTime(registro["ac_inicio"].ToString());
                _fin = Convert.ToDateTime(registro["ac_fin"].ToString());
                _esAlum = Convert.ToBoolean(int.Parse(registro["ac_esAlumno"].ToString()));
                _espId = new Espera(int.Parse(registro["ac_esp_id"].ToString()));

                _empId = new Empleado(int.Parse(registro["ac_emp_id"].ToString()));
                _cajaId = new Caja(int.Parse(registro["ac_caja_id"].ToString()));

            }
            else
            {
                _id = 0;
                _fecha = new DateTime();
                _inicio = new DateTime();
                _fin = new DateTime();
                _esAlum = false;
                _espId = new Espera();
                _empId = new Empleado();
                _cajaId = new Caja();
            }
 
        }

        public bool Agregar()
        {
            string instruccion = "insert into AtencionClientes(ac_fecha, ac_inicio, ac_fin, ac_esAlumno, ac_esp_id, ac_emp_id, ac_caja_id) values(@fecha, @inicio, @fin, @esAlumno, @espera, @empleado, @caja)";
            SqlCommand comando = new SqlCommand(instruccion);
            comando.Parameters.Add(new SqlParameter("@fecha", _fecha));
            comando.Parameters.Add(new SqlParameter("@inicio", _inicio));
            comando.Parameters.Add(new SqlParameter("@fin", _fin));
            comando.Parameters.Add(new SqlParameter("@esAlumno", _esAlum));
            comando.Parameters.Add(new SqlParameter("@espera", _espId.ID));
            comando.Parameters.Add(new SqlParameter("@empleado", _empId.Id));
            comando.Parameters.Add(new SqlParameter("@caja", _cajaId.Id));

            return EjecutarComando(comando);
        }


        #endregion

    }
}
