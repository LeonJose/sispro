using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
