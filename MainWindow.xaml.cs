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

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuItem_Home(object sender, RoutedEventArgs e)
        {
            AgendarCitas agendarCitas = new AgendarCitas();
            agendarCitas.Close();
            this.Show();
        }
        private void Boton_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Boton_Agendar_Cita(object sender, RoutedEventArgs e)
        {
            AgendarCitas agendarCitas = new AgendarCitas();
            this.Hide();
            agendarCitas.Show();
        }
        private void Boton_Eliminar_Cita(object sender, RoutedEventArgs e)
        {
            EliminarCita eliminarCita = new EliminarCita();
            this.Hide();
            eliminarCita.Show();
        }
        private void Boton_Ver_Cita(object sender, RoutedEventArgs e)
        {
            VerCita verCita = new VerCita();
            this.Hide();
            verCita.Show();
        }
    }
}