using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


    public class Conexion
    {
        // string de conexion
        /*private static string stringconexion = System.Configuration.ConfigurationSettings.AppSettings["cadenaConexion"].ConnectionString; /* ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;*/
        private static string stringconexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

        // objeto de conexion
        private static SqlConnection conexion;

        /// <summary>
        /// Conectar a servidor de Microsoft SQL Server
        /// </summary>
        /// <returns>Regresa true si se pudo establecer la conexión, false si hubo algun error</returns>
        private static bool Conectar()
        {
            bool conectado = false; // conexion todavia no establecida
            conexion = new SqlConnection(stringconexion);// inicializar conexion
            try
            {
                conexion.Open(); // abrir conexion
                conectado = true; // se establecio conexion
            }
            catch (SqlException)
            {
            }
            return conectado; // regresar estado de conexion
        }

        /// <summary>
        /// Cerrar conexión a Microsoft SQL Server, Liberar recursos
        /// </summary>
        private static void Desconectar()
        {
            conexion.Close(); // cerrar conexion
            conexion.Dispose(); // destruir, liberar recursos
            conexion = null; // liberar recursos
        }

        /// <summary>
        /// Ejecuta una consulta de SQL (query) y regresa la tabla resultante
        /// </summary>
        /// <param name="consulta">Consulta de SQL</param>
        /// <returns>Regresa la tabla de resultados, nula si hubo algun error en la consulta</returns>
        public static DataTable LeerTabla(string consulta)
        {
            DataTable tabla = new DataTable(); // tabla de resultados
            if (Conectar()) // conectar a Microsoft SQL Server
            {
                SqlCommand comando = new SqlCommand(consulta, conexion); // comando de SQL Server
                SqlDataAdapter adaptador = new SqlDataAdapter(comando); // adaptador
                try
                {
                    adaptador.Fill(tabla); // ejecuta Comando de SQL y llena la tabla virtual con el resultado
                    Desconectar(); // cerrar conexion
                }
                catch (SqlException) { }
            }
            return tabla; // regresar tabla de resultado
        }

        /// <summary>
        /// Busca un registro utilizando una consulta de SQL (query)
        /// </summary>
        /// <param name="consulta">Consulta SQL para leer el registro deseado</param>
        /// <returns>Regresa el registro encontrado o nulo si no se encontró</returns>
        public static DataRow LeerRegistro(string consulta)
        {
            DataTable tabla = LeerTabla(consulta); //leer tabla
            if (tabla.Rows.Count > 0) //si se encontró registro
                return tabla.Rows[0]; //regresar primer registro
            else
                return null; //regresar nulo
        }

        /// <summary>
        /// Ejecuta un comando de SQL no query (Insert, Update, Delete) sencillo (solo texto)
        /// </summary>
        /// <param name="instruccion">Instrucción de SQL</param>
        /// <returns>Regresa true si se pudo ejecutar, false si no</returns>
        public static bool EjecutarComando(string instruccion)
        {
            bool ejecuto = false;
            if (Conectar()) //conectar a Microsoft SQL Server
            {
                SqlCommand comando = new SqlCommand(instruccion, conexion); // comando
                try
                {
                    comando.ExecuteNonQuery(); // ejecutar comando SQL no query
                    ejecuto = true; // si se pudo ejecutar
                }
                catch (SqlException) { }
            }
            return ejecuto; //regresar valor
        }

        /// <summary>
        /// Ejecuta un comando de SQL no query (Insert, Update, Delete) con parámetros
        /// </summary>
        /// <param name="instruccion">Comando SQL</param>
        /// <returns>Regresa true si se pudo ejecutar, false si no</returns>
        public static bool EjecutarComando(SqlCommand comando)
        {
            bool ejecuto = false;
            if (Conectar()) //conectar a Microsoft SQL Server
            {
                comando.Connection = conexion; //ligar comando con conexión
                try
                {
                    comando.ExecuteNonQuery(); // ejecutar comando SQL no query
                    ejecuto = true; // si se pudo ejecutar
                }
                catch (SqlException ex) { }
            }
            return ejecuto; //regresar valor
        }
        /// <summary>
        /// Regresa valor int
        /// </summary>
        /// <param name="Comando"></param>
        /// <returns></returns>
        public static int EjecutarProcedimientoInt(SqlCommand Comando)
        {
            if (Conectar()) //conectar a Microsoft SQL Server
            {
                Comando.Connection = conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    Comando.ExecuteNonQuery();

                }
                catch (SqlException) { }

            }
            return Int32.Parse(Comando.Parameters["@resultado"].Value.ToString()); //regresar valor
        }

        /// <summary>
        /// Regresa valor string
        /// </summary>
        /// <param name="Comando"></param>
        /// <returns></returns>
        public static string EjecutarProcedimientoString(SqlCommand Comando)
        {
            if (Conectar()) //conectar a Microsoft SQL Server
            {
                Comando.Connection = conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    Comando.ExecuteNonQuery();

                }
                catch (SqlException) { }

            }
            return Comando.Parameters["@resultado"].Value.ToString(); //regresar valor
        }
        /// <summary>
        /// Regresa valor string
        /// </summary>
        /// <param name="Comando"></param>
        /// <returns></returns>
        public static object EjecutarProcedimiento(SqlCommand Comando)
        {

            if (Conectar()) //conectar a Microsoft SQL Server
            {
                Comando.Connection = conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    Comando.ExecuteNonQuery();

                }
                catch (SqlException) { }

            }
            return Comando.Parameters["@resultado"]; //regresar valor
        }
    }




