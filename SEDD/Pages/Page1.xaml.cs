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
using SEDD.Pages;
using SEDD.Scripts;
using MySql.Data.MySqlClient;

namespace SEDD
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        Consultas con;
        //Frame MFrame;
        public Page1(Consultas con)
        {
            this.con = con;
            Docente d = con.d;
            InitializeComponent();
            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            LabelNombre.Content = d.getNombre();
            LabelRFC.Content = d.getRFC();
            LabelArea.Content = d.getDepartamento();
            LabelCorreo.Content = d.getCorreo();

            if (!con.consultaRol())
            {
                menubtn.Visibility = Visibility.Hidden;
                datosbtn.Margin = new Thickness(370, 5, 0, 0);
                reqbtn.Margin = new Thickness(570, 5, 0, 0);
                reqprbtn.Margin = new Thickness(770, 5, 0, 0);
            }
            //this.MFrame = f;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (con.consultaRol())
            {
                MFrame.Content = new ModePage(con);
                MFrame = null;
                con = null;
            }
            else
            {
                MFrame.Content = new LoginPage();
                MFrame = null;
                con = null;
            }
            
        }

        private void Subir_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new SubirPage(con);
            MFrame = null;
            con = null;
        }

        private void Req_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new SubirReq(con);
            MFrame = null;
            con = null;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
            con = null;
        }

        private void Menu_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModePage(con);
            MFrame = null;
            con = null;
        }
    }
}
