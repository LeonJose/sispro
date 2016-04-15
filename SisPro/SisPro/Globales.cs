using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisPro
{
    public class Globales
    {
        public static Caja c = new Caja(1);
        public static Empleado emp = new Empleado(1);
        public static Configuracion conf = c.Configuracion;

    }
}
