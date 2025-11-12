using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
namespace winLogin
{
    /// <summary>
    /// Lógica de interacción para winSignUp.xaml
    /// </summary>
    public partial class winSignUp : Window
    {
        private readonly string rutaYnombreArch = "c:\\datosUsuario\\datosUsr.txt";
        public winSignUp()
        {
            InitializeComponent();
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)

        {

            txbNombre.Clear();

            txbEmail.Clear();

            txbCelular.Clear();

            pwdContraseña.Password = "";

        }

        private void txbNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexNombre = new Regex("^[a-zA-Z]$");
            e.Handled = !regexNombre.IsMatch(e.Text);
        }
        private void txbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexEmail = new Regex("^[a-zA-Z0-9@.]$");
            e.Handled = !regexEmail.IsMatch(e.Text);
        }
        private void txbCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexCelular = new Regex("^[0-9-+/]$");
            e.Handled = !regexCelular.IsMatch(e.Text);
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txbNombre.Text == "" || txbApPat.Text == "" || txbApMat.Text == "" || txbEmail.Text == "" || txbCelular.Text == "" || txbAñoNac.Text == "" || pwdContraseña.Password == "")
            {
                lblMensajes.Content = "Debe Ingresar todos los Datos";
                lblMensajes.Foreground = Brushes.Red;
            }
            else if(pwdContraseña.Password.Length < 8)
            {
                lblMensajes.Content = "La contraseña debe tener al menos 8 caracteres";
                lblMensajes.Foreground = Brushes.Red;
            }
            else
            {
                int año = int.Parse(txbAñoNac.Text);
                if (año <= 1950 || año >= 2007)
                {
                    lblMensajes.Content = "El año debe de ser de 1950 a 2007";
                    lblMensajes.Foreground = Brushes.Red;
                }
                else
                {
                    try
                    {
                        lblMensajes.Content = "Bienvenido al sistemas NN " + txbNombre.Text + "...";
                        lblMensajes.Foreground = Brushes.Black;
                        string datos = txbNombre.Text + "," + txbApPat.Text + "," + txbApMat.Text + "," +
                            txbEmail.Text + "," + txbCelular.Text + "," + txbAñoNac.Text + "," + pwdContraseña.Password + "\n";
                        File.AppendAllText(rutaYnombreArch, datos);
                        winPrincipal winPrincipal = new winPrincipal();
                        winPrincipal.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el archivo" + ex.Message);
                    }
                }
            }
        }
    }
}
