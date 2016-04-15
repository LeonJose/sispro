using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SisPro
{
    public class Caja:Conexion
    {
        private int _id;
        private int _numero;
        private Departamento _departamento;
        private Configuracion _configuracion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public Departamento Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        public Configuracion Configuracion
        {
            get { return _configuracion; }
            set { _configuracion = value; }
        }

        public Caja()
        {
            _id = 0;
            _numero = 0;
            _departamento = new Departamento();
            _configuracion = new Configuracion();
        }



        public Caja(int id)
        {
            string consulta = "select caja_numero, caja_dep_id, caja_conf_id from cajas where caja_id=" + id;
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                _id = id;
                _numero = int.Parse(registro["caja_numero"].ToString());
                _departamento = new Departamento(int.Parse(registro["caja_dep_id"].ToString()));
                _configuracion = new Configuracion(int.Parse(registro["caja_conf_id"].ToString()));
            }
            else
            {
                _id = 0;
                _numero = 0;
                _departamento = new Departamento();
                _configuracion = new Configuracion();
            }
        }

        //public bool AgregarCaja()
        //{
        //    string instruccion = "insert into cajas(caja_numero, caja_dep_id) values(@numero,@departamento)";
        //}
    }
}
