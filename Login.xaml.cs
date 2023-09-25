using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed) 
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM Cliente WHERE Cli_Usuario = @Usuario AND Cli_Contrasena = @Contrasena";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Usuario",txtUsuario.Text);
                sqlCmd.Parameters.AddWithValue("@Contrasena", txtContrasena.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrecto.");
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryUsuario = "SELECT Cli_ID FROM Cliente Where Cli_Usuario = '" + txtUsuario.Text + "'";
                SqlCommand sqlCmdUsuario= new SqlCommand(queryUsuario, sqlCon);
                SqlDataReader readerUsuario = sqlCmdUsuario.ExecuteReader();
                string idCliente = "";
                while (readerUsuario.Read())
                {
                    idCliente = readerUsuario.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string queryIdCliente = "Insert Into Cita (Cli_ID, Doc_ID, FD_ID) Values (@Cli_ID, @Doc_ID, @FD_ID)";
                SqlCommand comando = new SqlCommand(queryIdCliente, sqlCon);
                comando.Parameters.AddWithValue("@Cli_ID", idCliente);
                comando.Parameters.AddWithValue("@Doc_ID", idCliente);
                comando.Parameters.AddWithValue("@FD_ID", idCliente);
                comando.ExecuteNonQuery();
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
        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            NuevoUsuario nuevoUsuario = new NuevoUsuario();
            this.Hide();
            nuevoUsuario.Show();
        }
    }
}
