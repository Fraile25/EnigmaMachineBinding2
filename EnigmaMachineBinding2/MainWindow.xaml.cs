using Binding3;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnigmaMachineBinding2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MensajeManager mensajeManager = new MensajeManager();
            
            this.DataContext = mensajeManager;
            
        }

        private void btnVaciarCampos_Click(object sender, RoutedEventArgs e)
        {
            EnigmaManager enigmaManager = new EnigmaManager();

            TbMensajeCifrado.Text = string.Empty;
            TbMensajeClaro.Text = string.Empty;
            TbClave.Text = "aaa";
            enigmaManager.InicializarRotores();
        }

        private void btnCopiarPortapapeles_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbMensajeCifrado.Text.ToString());
        }

        
    }
}
