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

namespace SisPro
{
    /// <summary>
    /// Lógica de interacción para Atencion.xaml
    /// </summary>
    public partial class Atencion : Window
    {
        public Atencion()
        {
            InitializeComponent();
            headerlogo();
            txtnombre.Focus();
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
    }
}
