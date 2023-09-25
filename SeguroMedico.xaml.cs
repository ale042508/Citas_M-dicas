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
    /// Interaction logic for SeguroMedico.xaml
    /// </summary>
    public partial class SeguroMedico : Window
    {
        public SeguroMedico()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void MenuItem_Back(object sender, RoutedEventArgs e)
        {
            NuevoUsuario nuevoUsuario = new NuevoUsuario();
            this.Close();
            nuevoUsuario.Show();
        }

        private void MenuItem_Login(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
        private void btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NuevoUsuario nuevoUsuario = new NuevoUsuario();
                string activo = "Insert Into Seguros (Seg_Nombre, Seg_No_Afiliado, Seg_Tipo_Plan, Seg_Activo) Values (@Seg_Nombre, @Seg_No_Afiliado, @Seg_Tipo_Plan, @Seg_Activo)";
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand comandoActivo = new SqlCommand(activo, sqlCon);
                comandoActivo.Parameters.AddWithValue("@Seg_Nombre", txtSeguro.Text);
                comandoActivo.Parameters.AddWithValue("@Seg_No_Afiliado", txtNoAfiliado.Text);
                comandoActivo.Parameters.AddWithValue("@Seg_Tipo_Plan", txtTipoSeguro.Header);
                comandoActivo.Parameters.AddWithValue("@Seg_Activo", "A");
                comandoActivo.ExecuteNonQuery();
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
        private void Basico_Click(object sender, RoutedEventArgs e)
        {
            txtTipoSeguro.Header = Basico.Header;
        }
        private void Complementario_Click(object sender, RoutedEventArgs e)
        {
            txtTipoSeguro.Header = Complementario.Header;
        }
    }
}
