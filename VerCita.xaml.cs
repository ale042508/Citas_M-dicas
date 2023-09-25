using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for VerCita.xaml
    /// </summary>
    public partial class VerCita : Window
    {
        public VerCita()
        {
            InitializeComponent();
            Ver_Cita();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void MenuItem_Home(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
        void Ver_Cita()
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdCita = "SELECT Cli_ID FROM Cita Where Cita_ID=(Select max(Cita_ID) From Cita";
                SqlCommand sqlCmdIdCita = new SqlCommand(queryIdCita, sqlCon);
                SqlDataReader readerIdCita = sqlCmdIdCita.ExecuteReader();
                string idCita = "";
                while (readerIdCita.Read())
                {
                    idCita = readerIdCita.GetString(0);
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdDoctor = "SELECT Doc_ID FROM Cita Where Cita_ID=(Select max(Cita_ID) From Cita";
                SqlCommand sqlCmdIdDoctor = new SqlCommand(queryIdDoctor, sqlCon);
                SqlDataReader readerIdDoctor = sqlCmdIdDoctor.ExecuteReader();
                string IdDoctor = "";
                while (readerIdDoctor.Read())
                {
                    IdDoctor = readerIdDoctor.GetString(0);
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdFecha = "SELECT FD_ID FROM Cita Where Cita_ID=(Select max(Cita_ID) From Cita";
                SqlCommand sqlCmdIdFecha = new SqlCommand(queryIdFecha, sqlCon);
                SqlDataReader readerIdFecha = sqlCmdIdFecha.ExecuteReader();
                string IdFecha = "";
                while (readerIdFecha.Read())
                {
                    IdFecha = readerIdFecha.GetString(0);
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryNombreCliente = $"SELECT Cli_Nombre, Cli_Apellido FROM Cliente Where Cli_ID = {idCita}";
                SqlCommand sqlCmdNombreCliente = new SqlCommand(queryNombreCliente, sqlCon);
                SqlDataReader readerNombreCliente = sqlCmdNombreCliente.ExecuteReader();
                while (readerNombreCliente.Read())
                {
                    string NombreCliente = readerNombreCliente.GetString(0);
                    string apellidoCliente = readerNombreCliente.GetString(1);
                    lb_VerCita.Items.Add($"Cita Programada para: {NombreCliente} {apellidoCliente}\n");
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryDoctor = $"SELECT Doc_Nombre FROM Doctores Where Doc_ID = {IdDoctor}";
                SqlCommand sqlCmdDoctor = new SqlCommand(queryDoctor, sqlCon);
                SqlDataReader readerDoctor = sqlCmdDoctor.ExecuteReader();
                while (readerDoctor.Read())
                {
                    string Doctor = readerDoctor.GetString(0);
                    lb_VerCita.Items.Add($"Doctor: {Doctor}\n");
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryFechaDisponible = $"SELECT FD_Dia, FD_Tanda FROM FechaDisponible Where FD_ID = {IdFecha}";
                SqlCommand sqlCmdFechaDisponible = new SqlCommand(queryFechaDisponible, sqlCon);
                SqlDataReader readerFechaDisponible = sqlCmdFechaDisponible.ExecuteReader();
                while (readerFechaDisponible.Read())
                {
                    string Dia = readerFechaDisponible.GetString(0);
                    string Tanda = readerFechaDisponible.GetString(1);
                    lb_VerCita.Items.Add($"Día: {Dia}, Tanda: {Tanda}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
