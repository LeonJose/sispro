using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data;
using System.Data.SqlClient;
using System.Data;

namespace SisPro
{
    public class Espera : Conexion
    {
      
      #region Atributos
      private int _id;
      private string _nombre;
      private string _numero;
      private DateTime _fecha;
      private DateTime _horaLlegada;
      private DateTime _horaAtencion;
      private string _matricula;
      private Departamento _departamento;
      private bool _atendido;
     // private string impresora = "Microsoft XPS Document Writer";
      private string impresora = Globales.conf.Impresora;
      #endregion

        #region propiedades

      public Departamento Departamento
      {
          get { return _departamento; }
          set { _departamento = value; }
      }

      public int ID
      {
          get { return _id; }
          set { _id = value; }
      }
      public string Nombre
      {
          get { return _nombre; }
          set { _nombre = value; }
      }
      public string Numero
      {
          get { return _numero; }
          set { _numero = value; }
      }
      public DateTime Fecha
      {
          get { return _fecha; }
          set { _fecha = value; }
      }
      public DateTime HoraLlegada
      {
          get { return _horaLlegada; }
          set { _horaLlegada = value; }
      }
      public DateTime HoraAtencion
      {
          get { return _horaAtencion; }
          set { _horaAtencion = value; }
      }
      public string Matricula
      {
          get { return _matricula; }
          set { _matricula = value; }
      }

      public bool Atendido
      {
          get { return _atendido; }
          set { _atendido = value; }
      }
        #endregion

        #region constructor
      public Espera()
      {
          _id = 0;
          _nombre = " ";
          _numero = " ";
          _fecha = new DateTime();
          _horaLlegada = new DateTime();
          _horaAtencion = new DateTime();
          _matricula = "";
          _departamento = new Departamento();
          _atendido = false;
      }
        #endregion 
        #region Metodos


        public Espera(int id)
        {
            string consulta = "select esp_nombre, esp_numero, esp_fecha, esp_horaLlegada, esp_horaAtencion, esp_matricula, esp_dep_id, esp_atendido from Espera where esp_id="+id;
            DataRow registro = LeerRegistro(consulta);
            if (registro != null)
            {
                _id = id;
                _nombre = registro["esp_nombre"].ToString();
                _numero = registro["esp_numero"].ToString();
                _fecha = DateTime.Parse(registro["esp_fecha"].ToString());
                _horaLlegada = DateTime.Parse(registro["esp_horaLlegada"].ToString());
                _horaAtencion = DateTime.Parse(registro["esp_horaAtencion"].ToString());
                _matricula = registro["esp_matricula"].ToString();
                _departamento = new Departamento(int.Parse(registro["esp_dep_id"].ToString()));
                _atendido = Convert.ToBoolean(registro["esp_atendido"].ToString());
            }
            else
            {
                _id = 0;
                _nombre = " ";
                _numero = " ";
                _fecha = new DateTime();
                _horaLlegada = new DateTime();
                _horaAtencion = new DateTime();
                _matricula = "";
                _departamento = new Departamento();
                _atendido = false;
            }
        }

      public bool AgregarEspera()
      {
          string instruccion = @"insert into Espera(esp_nombre, esp_numero, esp_fecha, esp_horaLlegada, esp_horaAtencion, esp_matricula, esp_dep_id) values 
                                (@nombre, @num, @fec, @hLlegada, @hAtendido,@mat, @dept)";
          SqlCommand comandoSql = new SqlCommand(instruccion);
          comandoSql.Parameters.Add(new SqlParameter("@nombre", _nombre));
          comandoSql.Parameters.Add(new SqlParameter("@num", _numero));
          comandoSql.Parameters.Add(new SqlParameter("@fec", _fecha));
          comandoSql.Parameters.Add(new SqlParameter("@hLlegada", _horaLlegada));
          comandoSql.Parameters.Add(new SqlParameter("@hAtendido", _horaAtencion));
          comandoSql.Parameters.Add(new SqlParameter("@mat", _matricula));
          comandoSql.Parameters.Add(new SqlParameter("@dept", _departamento.IdDepto));
          return EjecutarComando(comandoSql);
      }

      public bool AtenderEspera()
      {
          string instruccion = "update Espera set esp_atendido="+_atendido+" where esp_id=" + _id;
          SqlCommand comando = new SqlCommand(instruccion);

          return EjecutarComando(comando);
      }


      public void GenerarTicket()
      {
          try
          {
              //StreamReader stream = new StreamReader(@"./codigo.png");

              //PictureBox pb2 = new PictureBox();
              Ticket ticket = new Ticket();
              //pb2.Image = Image.FromStream(stream.BaseStream);
              ticket.HeaderImage = SisPro.Properties.Resources.logo;

              ticket.AddDatos("=================", "==================");
              ticket.AddDatos("", "");
              ticket.AddDatos("Nombre:  ", "");
              ticket.AddDatos("", Nombre);
              if (Matricula != "")
              {
                  ticket.AddDatos("Matricula:", "");
                  ticket.AddDatos("", Matricula);
              }
              ticket.AddDatos("Departamento:  ", "");
              ticket.AddDatos("", Departamento.NombreDepto);
              ticket.AddDatos("Fecha:", "");
              ticket.AddDatos("", Fecha.ToString("MM-dd-yyyy"));
              ticket.AddDatos("Hora:", "");
              ticket.AddDatos("", HoraLlegada.ToString("HH:mm:ss"));
              ticket.AddDatos("=================", "==================");
              ticket.AddTitulo("No #"+Numero);
              ticket.AddFooter("=================", "==================");
              //ticket.FooterImage = pb2.Image;
              ticket.PrintTicket(impresora);

              //ticket.PrintTicket();
              //stream.Close();

          }
          catch (Exception e)
          {
          }
      }

        #endregion

    }
}
