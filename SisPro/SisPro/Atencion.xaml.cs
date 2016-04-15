using System;
using System.Collections.Generic;
using System.Data;
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

namespace SisPro
{
    /// <summary>
    /// Lógica de interacción para Atencion.xaml
    /// </summary>
    public partial class Atencion : Window
    {
        public List<Espera> lista;
        public Espera es;
        public DateTime inicio;

        public Atencion()
        {
            InitializeComponent();
            Inicializar();
        }

        public void Inicializar()
        {
            headerlogo();
            txtNombre.Focus();
            //ActualizarListaDeEspera();
            lista = Listados.ListaDeEspera();
            if (lista.Count > 0)
                dgvListaEspera.ItemsSource = lista;
        }
        private void headerlogo()
        {
            Image _image = new Image();
            BitmapImage _bi = new BitmapImage();
            _bi.BeginInit();
            _bi.UriSource = new System.Uri("pack://application:,,,/Recursos/imagenes/1.png");
            _bi.EndInit();
            _image.Source = _bi;
            ImageBrush _ib = new ImageBrush();
            _ib.ImageSource = _bi;
            rt_imagen.Fill = _ib;
        }


        

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Listados.SiguienteEspera().ID != 0)
            {
                es = Listados.SiguienteEspera();
                txtNumero.Text = es.Numero;
                txtNombre.Text = es.Nombre;
                txtHora.Text = es.HoraLlegada.ToString("HH:mm:ss");
                inicio = DateTime.Now;
                lista = Listados.ListaDeEspera();
                es.Atendido = true;
                es.AtenderEspera();
                if (lista.Count > 0)
                    dgvListaEspera.ItemsSource = lista;

                Globales.conf.Espera = es.ID;
                Globales.conf.EditarConfiguracion();

                Listados.LlamarSiguiente(es.ID);
            }
            else
            {
                MessageBox.Show("No hay personas en espera", "No hay Espera", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNombre.Text != "" && txtNumero.Text != "" && txtHora.Text != "" && txtAsunto.Text != "")
            {
                btnEnviarDatos.IsEnabled = true;
            }
            else
            {
                btnEnviarDatos.IsEnabled = false;
            }
        }

        private void txtAsunto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (txtNombre.Text != "" && txtNumero.Text != "" && txtHora.Text != "" && txtAsunto.Text != "")
                {
                    btnEnviarDatos.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else
                {
                    MessageBox.Show("Debe llenar todos los campos", "Datos Incompletos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void btnEnviarDatos_Click(object sender, RoutedEventArgs e)
        {
            if (es.ID != 0)
            {
                AtencionCliente ac = new AtencionCliente();
                ac.Espera = es;
                ac.CajaId = Globales.c;
                ac.EmpleadoId = Globales.emp;
                if (es.Matricula != "")
                    ac.EsAlumno = true;
                else
                    ac.EsAlumno = false;
                ac.Fecha = DateTime.Now;
                ac.Inicio = inicio;
                ac.Fin = DateTime.Now;
                if (ac.Agregar())
                {
                    txtNumero.Clear();
                    txtNombre.Clear();
                    txtHora.Clear();
                    txtAsunto.Clear();
                    es.Atendido = true;
                    es.AtenderEspera();
                    Globales.conf.Espera = 0;
                    Globales.conf.EditarConfiguracion();

                    Listados.LlamarSiguiente(0);

                    lista = Listados.ListaDeEspera();
                    if (lista.Count > 0)
                        dgvListaEspera.ItemsSource = lista;

                }
                else
                {
                    MessageBox.Show("No se pudieron guardar los datos", "Error al guardar datos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
