using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SisPro
{
    class Alumno : Conexion
    {
        #region Atributos

        private int _id;
        private string _matricula;
        private string _nombre;
        private string _apaterno;
        private string _amaterno;
        private DateTime _fechaNacimiento;
        private string _sexo;
        private string _carrera;

        #endregion

        #region Propiedades

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
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
        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }
        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }
        public string Carrera
        {
            get { return _carrera; }
            set { _carrera = value; }
        }
        #endregion

        #region Contructor

        public Alumno()
        {
            _id = 0;
            _matricula = " ";
            _nombre = " ";
            _apaterno = " ";
            _amaterno = " ";
            _fechaNacimiento = new DateTime();
            _sexo = " ";
            _carrera = " ";
        }

        #endregion 

        #region Metodos

        public Alumno(string matri)
        {
            DataRow dr = LeerRegistro("select alu_id,alu_nombre, alu_apaterno, alu_amaterno, alu_fechaNacimiento, alu_sexo, alu_carrera  from Alumnos where alu_matricula = '"+ matri +"'");
            if (dr != null)
            {
                _nombre = dr["alu_nombre"].ToString();
                _apaterno = dr["alu_apaterno"].ToString();
                _amaterno = dr["alu_amaterno"].ToString();
                _fechaNacimiento = (DateTime)dr["alu_fechaNacimiento"];
                _sexo = dr["alu_sexo"].ToString();
                _carrera = dr["alu_carrera"].ToString();
                _matricula = matri;
            }
            else
            {
                _nombre = "";
                _apaterno = "";
                _amaterno = "";
                _fechaNacimiento = new DateTime();
                _sexo = "";
                _carrera = "";
                _matricula = "";
            }
        }
        public bool AgregarAlumno()
        {
            string instruccion = @"insert into alumnos(alu_matricula, alu_nombre, alu_apaterno, alu_amaterno,alu_fechanacimiento, alu_sexo,alu_carrera) values (@matricula,@nombre,@apaterno,@amaterno,@fecha,@sex,@carrera)";
            SqlCommand comandoSql = new SqlCommand(instruccion);
            comandoSql.Parameters.Add(new SqlParameter("@matricula", _matricula));
            comandoSql.Parameters.Add(new SqlParameter("@nombre", _nombre));
            comandoSql.Parameters.Add(new SqlParameter("@apaterno", _apaterno));
            comandoSql.Parameters.Add(new SqlParameter("@amaterno", _amaterno));
            comandoSql.Parameters.Add(new SqlParameter("@fecha", _fechaNacimiento));
            comandoSql.Parameters.Add(new SqlParameter("@sex", _sexo));
            comandoSql.Parameters.Add(new SqlParameter("@carrera", _carrera));
            return EjecutarComando(comandoSql);
        }


        #endregion



    }
}
