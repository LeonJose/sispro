using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
    }
}
