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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace winLogin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string rutaYnombreArch = "c:\\datosUsuario\\datosUsr.txt";

        public MainWindow()

        {

            InitializeComponent();

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)

        {

            txbEmail.Clear();

            pwdContraseña.Password = "";

        }

        private void txbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {

            Regex regexEmail = new Regex("^[a-zA-Z0-9@.]$");

            e.Handled = !regexEmail.IsMatch(e.Text);

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)

        {

            if (txbEmail.Text == "" || pwdContraseña.Password == "")

            {

                lblMensajes.Content = "Debe completar TODOS los datos";

                lblMensajes.Foreground = Brushes.Red;

            }

            else

            {

                try

                {


                    string correo = txbEmail.Text;

                    string contra = pwdContraseña.Password;

                    if (!File.Exists(rutaYnombreArch))

                    {

                        lblMensajes.Foreground = Brushes.Red;

                        lblMensajes.Content = "La ruta o nombre del archivo no existen";

                        return;

                    }

                    var lineas = File.ReadAllLines(rutaYnombreArch);

                    bool encontrado = false;

                    foreach (var unaLinea in lineas)

                    {

                        var partes = unaLinea.Split(',');

                        if (correo.Equals(partes[3]) && contra.Equals(partes[6]))

                        {

                            encontrado = true;

                        }

                    }

                    if (encontrado)

                    {

                        lblMensajes.Foreground = Brushes.Black;

                        lblMensajes.Content = "Bienvenido al sistema NN " + "...";

                        winPrincipal winP = new winPrincipal();

                        winP.Show();

                        this.Close();

                    }

                    else

                    {

                        MessageBox.Show("USUARIO NO REGISTRADO...");

                        txbEmail.Clear();

                        pwdContraseña.Clear();

                    }

                    string carpeta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    string rutaArchivo = System.IO.Path.Combine(carpeta, "usuarios.txt");



                    System.IO.File.AppendAllText(rutaArchivo, "\n");


                }

                catch (Exception ex)

                {

                    MessageBox.Show("Error al guardar el archivo: " + ex.Message,

                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }

        }


        private void btnRegistro_Click(object sender, RoutedEventArgs e)

        {

            winSignUp winSignUp = new winSignUp();

            winSignUp.Show();

            this.Close();

        }

    }
}
