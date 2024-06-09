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
using MySql.Data.MySqlClient;
using SEDD.Scripts;

namespace SEDD.Pages
{
    /// <summary>
    /// Lógica de interacción para ModePage.xaml
    /// </summary>
    public partial class ModePage : Page
    {
        Consultas con;
        //Frame xf;
        public ModePage(Consultas con)
        {
            this.con = con;
            InitializeComponent();
            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;
            //xf = f;
        }

        private void docente_modeOn(object sender, RoutedEventArgs e)
        {
            ModeFrame.Content = new Page1(con);
            ModeFrame = null;
            con = null;
        }

        private void revisor_modeOn(object sender, RoutedEventArgs e)
        {
            ModeFrame.Content = new MainRevisor(con);
            ModeFrame = null;
            con = null;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            ModeFrame.Content = new LoginPage();
            ModeFrame = null;
            con = null;
        }
    }
}
