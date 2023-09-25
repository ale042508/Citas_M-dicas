using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for AgendarCitas.xaml
    /// </summary>
    public partial class AgendarCitas : Window
    {
        public AgendarCitas()
        {
            InitializeComponent();
            fill_combo();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void MenuItem_Home(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
        void fill_combo()
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT Esp_ID, Esp_Nombre FROM Especialidades Order By Esp_Nombre";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(1);
                    cbbEspecialidades.Items.Add(name);
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
        private void cbbEspecialidades_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT Esp_ID FROM Especialidades Where Esp_Nombre = '"+cbbEspecialidades.Text+"'";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                string id = "";
                while (reader.Read())
                {
                    id = reader.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryDoctores = $"SELECT Doc_ID, Doc_Nombre FROM Doctores Where Esp_ID = {id} Order By Doc_Nombre";
                SqlCommand sqlCmdDoctores = new SqlCommand(queryDoctores, sqlCon);
                SqlDataReader readerDoctores = sqlCmdDoctores.ExecuteReader();
                cbbDoctores.Items.Clear();
                while (readerDoctores.Read())
                {
                    string name = readerDoctores.GetString(1);
                    cbbDoctores.Items.Add(name);
                }
                cbbDoctores.IsEnabled = true;
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
        private void cbbDoctores_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT Doc_ID FROM Doctores Where Doc_Nombre = '" + cbbDoctores.Text + "'";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                string id = "";
                while (reader.Read())
                {
                    id = reader.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryDoctores = $"SELECT FD_ID, FD_Dia, FD_Tanda FROM FechaDisponible Where Doc_ID = {id} Order By FD_Dia_Num";
                SqlCommand sqlCmdDoctores = new SqlCommand(queryDoctores, sqlCon);
                SqlDataReader readerDoctores = sqlCmdDoctores.ExecuteReader();
                cbbFecha.Items.Clear();
                while (readerDoctores.Read())
                {
                    string dia = readerDoctores.GetString(1);
                    string tanda = readerDoctores.GetString(2);
                    cbbFecha.Items.Add($"Día: {dia}, Tanda: {tanda}");
                }
                cbbFecha.IsEnabled = true;
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
        private void btn_CrearCita_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdDoctores = "SELECT Doc_ID FROM Doctores Where Doc_Nombre = '" + cbbDoctores.Text + "'";
                SqlCommand sqlCmdIdDoctores = new SqlCommand(queryIdDoctores, sqlCon);
                SqlDataReader readerIdDoctores = sqlCmdIdDoctores.ExecuteReader();
                string idDoctores = "";
                while (readerIdDoctores.Read())
                {
                    idDoctores = readerIdDoctores.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdFecha = $"SELECT FD_ID FROM FechaDisponible Where Doc_ID = {idDoctores}";
                SqlCommand sqlCmdIdFecha = new SqlCommand(queryIdFecha, sqlCon);
                SqlDataReader readerIdFecha = sqlCmdIdFecha.ExecuteReader();
                string IdFecha = "";
                while (readerIdFecha.Read())
                {
                    IdFecha = readerIdFecha.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdCita = $"SELECT Cli_ID FROM Cita WHERE Cita_ID=(SELECT max(Cita_ID) FROM Cita)";
                SqlCommand sqlCmdIdCita = new SqlCommand(queryIdCita, sqlCon);
                SqlDataReader readerIdCita = sqlCmdIdCita.ExecuteReader();
                string idCita = "";
                while (readerIdCita.Read())
                {
                    idCita = readerIdCita.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string queryIdCliente = $"Update Cita Set Doc_ID = @Doc_ID, FD_ID = @FD_ID Where Cita_ID = (SELECT max(Cita_ID) FROM Cita)";
                SqlCommand comando = new SqlCommand(queryIdCliente, sqlCon);
                comando.Parameters.AddWithValue("@Doc_ID", idDoctores);
                comando.Parameters.AddWithValue("@FD_ID", IdFecha);
                comando.ExecuteNonQuery();
                MessageBox.Show("Cita creada con éxito");
                this.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
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