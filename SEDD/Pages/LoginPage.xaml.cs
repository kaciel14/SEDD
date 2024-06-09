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
using SEDD.Scripts;

namespace SEDD.Pages
{
    /// <summary>
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Conexion c = new Conexion();
            Consultas con = new Consultas(c.getConection());
            int existe = con.BuscarUsuario(campoRFC.Text,campoPass.Password);
            

            if (existe.Equals(1))
            {
                con.setUsuario();
                if (con.consultaRol())
                {
                    MFrame.Content = new ModePage(con);
                    MFrame = null;
                    con = null;
                }
                else
                {
                    MFrame.Content = new Page1(con);
                    MFrame = null;
                    con = null;
                }
                
            }
            else if (existe.Equals(2))
            {
                MFrame.Content = new MainRepresentante(con);
                MFrame = null;
                con = null;
            }
            else
            {
                badLogin.Visibility = Visibility.Visible;
            }
        }

    }
}
