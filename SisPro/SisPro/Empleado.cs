using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace SisPro
{
    public class Empleado : Conexion
    {
        #region Atributos

        private int _id;
        private string _nombre;
        private string _apaterno;
        private string _amaterno;
        private string _usuario;
        private string _constrasena;
        private bool _supervisor;

        #endregion

        #region Propiedades

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apaterno
        {
            get { return _apaterno; }
            set { _apaterno = value; }
        }
        public string Amaterno
        {
            get { return _amaterno; }
            set { _amaterno = value; }
        }
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public string Contrasena
        {
            get { return _constrasena; }
            set { _constrasena = value; }
        }
        public bool Supervisor
        {
            get { return _supervisor; }
            set { _supervisor = value; }

        }

        #endregion

        #region Constructor
        public Empleado()
        {
            _id = 0;
            _nombre = "";
            _apaterno = "";
            _amaterno = "";
            _usuario = "";
            _constrasena = "";
            _supervisor = true;
        }

        #endregion

        #region Metodos
        
        public Empleado(int i)
        {
            DataRow dr = LeerRegistro("select emp_nombre, emp_apellidoPaterno,emp_apellidoMaterno,emp_usuario,emp_contrasena, emp_supervisor from Empleados where emp_id ='"+ i +"'");
            if (dr != null)
            {
                _nombre = dr["emp_nombre"].ToString();
                _apaterno = dr["emp_apellidoPaterno"].ToString();
                _amaterno = dr["emp_apellidoMaterno"].ToString();
                _usuario = dr["emp_usuario"].ToString();
                _constrasena = dr["emp_contrasena"].ToString();
               _supervisor = Convert.ToBoolean(dr["emp_supervisor"].ToString());
                _id = i;
            }
            else
            {
                _nombre = "";
                _apaterno = "";
                _amaterno = "";
                _usuario = "";
                _constrasena = "";
                _supervisor = true;
                _id = 0;
            }
        }

        public bool AgregarEmpleados()
        {
            string consulta = @"insert into Empleados(emp_nombre, emp_apellidoPaterno, emp_apellidoMaterno,emp_usuario,emp_contrasena, emp_supervisor) values (@nombre,@apaterno,@amaterno,@usuario,@contrasena,@supervisor)";
            SqlCommand comandoSql = new SqlCommand(consulta);
            comandoSql.Parameters.Add(new SqlParameter("@nombre", _nombre));
            comandoSql.Parameters.Add(new SqlParameter("@apaterno", _apaterno));
            comandoSql.Parameters.Add(new SqlParameter("@amaterno", _amaterno));
            comandoSql.Parameters.Add(new SqlParameter("@usuario", _usuario));
            comandoSql.Parameters.Add(new SqlParameter("@contrasena", _constrasena));
            comandoSql.Parameters.Add(new SqlParameter("supervisor", _supervisor));
            return EjecutarComando(comandoSql);
        }

        #endregion
    }
}
