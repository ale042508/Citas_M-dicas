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
using System.Data.SqlTypes;

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for NuevoUsuario.xaml
    /// </summary>
    public partial class NuevoUsuario : Window
    {
        public NuevoUsuario()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void MenuItem_Back(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
        private void btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsuario.Text != "" && txtPassword.Password != "" && txtNombres.Text != "" && txtApellidos.Text != "" && txtCelular.Text != "")
            {
                if (btn_Siguiente.Content.ToString() == "Siguiente")
                {
                    SeguroMedico seguroMedico = new SeguroMedico();
                    this.Hide();
                    seguroMedico.Show();
                }
                if (btn_Siguiente.Content.ToString() == "Crear Cuenta")
                {
                    try
                    {
                        string activo = "Insert Into Seguros (Seg_Nombre, Seg_No_Afiliado, Seg_Tipo_Plan, Seg_Activo) Values (@Seg_Nombre, @Seg_No_Afiliado, @Seg_Tipo_Plan, @Seg_Activo)";
                        string query = "Insert Into Cliente (Cli_Usuario, Cli_Contrasena, Cli_Nombres, Cli_Apellidos, Cli_Tel_Celular, Cli_Seguro) Values (@Cli_Usuario, @Cli_Contrasena, @Cli_Nombres, @Cli_Apellidos, @Cli_Tel_Celular, @Cli_Seguro)";
                        string idSeguro = "Select count(Seg_ID) From Seguros = @Seg_ID";
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        SqlCommand comandoIdSeguro = new SqlCommand(idSeguro, sqlCon);
                        comandoIdSeguro.CommandType = CommandType.Text;
                        comandoIdSeguro.Parameters.AddWithValue("@Seg_ID", idSeguro);

                        SqlCommand comandoActivo = new SqlCommand(activo, sqlCon);
                        comandoActivo.Parameters.AddWithValue("@Seg_Nombre", "");
                        comandoActivo.Parameters.AddWithValue("@Seg_No_Afiliado", "");
                        comandoActivo.Parameters.AddWithValue("@Seg_Tipo_Plan", "");
                        comandoActivo.Parameters.AddWithValue("@Seg_Activo", "I");
                        comandoActivo.ExecuteNonQuery();

                        SqlCommand comando = new SqlCommand(query, sqlCon);
                        comando.Parameters.AddWithValue("@Cli_Usuario", txtUsuario.Text);
                        comando.Parameters.AddWithValue("@Cli_Contrasena", txtPassword.Password);
                        comando.Parameters.AddWithValue("@Cli_Nombres", txtNombres.Text);
                        comando.Parameters.AddWithValue("@Cli_Apellidos", txtApellidos.Text);
                        comando.Parameters.AddWithValue("@Cli_Tel_Celular", txtCelular.Text);
                        comando.Parameters.AddWithValue("@Cli_Seguro", idSeguro);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Cuenta creada con satisfacción");
                        Login login = new Login();
                        this.Close();
                        login.Show();
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
                else
                {
                    try
                    {
                        string query = "Insert Into Cliente (Cli_Usuario, Cli_Contrasena, Cli_Nombres, Cli_Apellidos, Cli_Tel_Celular, Cli_Seguro) Values (@Cli_Usuario, @Cli_Contrasena, @Cli_Nombres, @Cli_Apellidos, @Cli_Tel_Celular, @Cli_Seguro)";
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        SqlCommand comando = new SqlCommand(query, sqlCon);
                        comando.Parameters.AddWithValue("@Cli_Usuario", txtUsuario.Text);
                        comando.Parameters.AddWithValue("@Cli_Contrasena", txtPassword.Password);
                        comando.Parameters.AddWithValue("@Cli_Nombres", txtNombres.Text);
                        comando.Parameters.AddWithValue("@Cli_Apellidos", txtApellidos.Text);
                        comando.Parameters.AddWithValue("@Cli_Tel_Celular", txtCelular.Text);
                        if (txtSeguro.IsChecked == true)
                        {
                            comando.Parameters.AddWithValue("@Cli_Seguro", "S");
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@Cli_Seguro", "N");
                        }
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
            }
        }
        private void txtSeguro_Checked(object sender, RoutedEventArgs e)
        {
            if (txtSeguro.IsChecked == true && btn_Siguiente.Content.ToString() == "Crear Cuenta")
            {
                btn_Siguiente.Content = "Siguiente";
            }
            else
            {
                btn_Siguiente.Content = "Crear Cuenta";
            }
        }
    }
}
