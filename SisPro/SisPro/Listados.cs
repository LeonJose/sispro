using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SisPro
{
    public class Listados: Conexion
    {
        public static int ObtenerNumero()
        {
            int i = 0;
            string consulta = "select count(esp_id) as cont from espera where esp_fecha= '"+DateTime.Now.ToString("yyyy-MM-dd")+"'";
            System.Data.DataRow registro = LeerRegistro(consulta);
            if(registro !=null)
            {
                i = int.Parse(registro["cont"].ToString())+1;
            }

            return i;
        }

        public static List<Departamento> Departamentos()
        {
            List<Departamento> depto = new List<Departamento>();
            string consulta = "SELECT dep_id from departamentos";
            DataTable tabla = LeerTabla(consulta);
            foreach (DataRow registro in tabla.Rows)
            {
                depto.Add(new Departamento(int.Parse(registro["dep_id"].ToString())));
            }
            return depto;
        }

        public static List<Espera> ListaDeEspera()
        {
            List<Espera> lista = new List<Espera>();
            string consulta = "select esp_id from Espera where esp_atendido=0";
            DataTable tabla = LeerTabla(consulta);
            foreach (DataRow registro in tabla.Rows)
            {
                lista.Add(new Espera(int.Parse(registro["esp_id"].ToString())));
            }

            return lista;
        }

        public static Espera SiguienteEspera()
        {
            Espera esp = new Espera();
            string consulta = "select esp_id from Espera where esp_horaLlegada=(select min(esp_horaLlegada) from Espera where esp_atendido=0)";
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                esp = new Espera(int.Parse(registro["esp_id"].ToString()));
            }


            return esp;
        }

        public static Espera Siguiente()
        {
            Espera esp = new Espera();
            string consulta = "select sig_espera from siguiente where sig_id=1";
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                if(registro["sig_espera"].ToString()!="0")
                {
                    esp = new Espera(int.Parse(registro["sig_espera"].ToString()));
                }
            }
            return esp;
        }

        public static void LlamarSiguiente(int espera)
        {
            string instruccion = "update siguiente set sig_espera="+espera+" where sig_id=1";
            SqlCommand comando = new SqlCommand(instruccion);
            EjecutarComando(comando);
        }
    }
}
