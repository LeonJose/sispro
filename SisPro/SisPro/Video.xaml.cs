using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace SisPro
{
    /// <summary>
    /// Lógica de interacción para Video.xaml
    /// </summary>
    public partial class Video : Window
    {
        public Espera siguiente = Listados.Siguiente();
        private DispatcherTimer tiempo = new DispatcherTimer();
        private DispatcherTimer cerrar = new DispatcherTimer();
        private DispatcherTimer time = new DispatcherTimer();

        public Video()
        {
            InitializeComponent();
            Element.LoadedBehavior = MediaState.Manual;
            Element.MediaEnded += new RoutedEventHandler(m_MediaEnded);
            Element.Source = new Uri(@"C:\Users\Trelloch\Desktop\3er y 4TO CUATRIMESTRE\ProyectoClinica4toCuatri\Clinica\Clinica\Videos\minions.mp4");

            Element.Play();


            tiempo.Interval = new TimeSpan(0, 2, 50);
            tiempo.IsEnabled = true;
            tiempo.Tick += new EventHandler(tiempo_tick);
            tiempo.Start();


            //cerrar.Interval = new TimeSpan(0, 0, 35);
            //cerrar.IsEnabled = true;
            //cerrar.Tick += new EventHandler(cerrar_tick);
            //cerrar.Start();

            time.Interval = new TimeSpan(0, 0, 5);
            time.IsEnabled = true;
            time.Tick += new EventHandler(time_tick);
            time.Start();
        }

        void m_MediaEnded(object sender, RoutedEventArgs e)
        {
            Element.Position = TimeSpan.FromSeconds(0);
            Element.Play();
        }

        private void tiempo_tick(object sender, EventArgs e)
        {
            try
            {
                Element.Play();
            }
            catch (Exception) { }
        }

        private void time_tick(object sender, EventArgs e)
        {
            try
            {
                siguiente = Listados.Siguiente();
                if (siguiente.ID != 0)
                {

                    myCheckBox.IsChecked = true;
                    var s = new MyData { Nombre = siguiente.Nombre, Numero = siguiente.Numero, CajaNumero = "Favor de pasar a Caja: "+Globales.c.Numero.ToString(), Departamento = Globales.c.Departamento.NombreDepto };
                    this.DataContext = s;

                }
                if (!Element.IsBuffering)
                {
                    Element.Play();
                }
            }
            catch (Exception) { }
        }

        public struct MyData
        {
            public string Nombre { set; get; }
            public string Numero { set; get; }
            public string CajaNumero { set; get; }
            public string Departamento { set; get; }
        }

        //private void cerrar_tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (siguiente.ID != 0)
        //        {
        //            myCheckBox.IsChecked = false;
        //            siguiente.Null();
        //            siguiente = null;

        //        }
        //    }
        //    catch (Exception) { }
        //}


    }
}
