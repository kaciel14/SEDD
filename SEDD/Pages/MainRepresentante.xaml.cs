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
    /// Lógica de interacción para MainRepresentante.xaml
    /// </summary>
    public partial class MainRepresentante : Page
    {
        Consultas con;

        public MainRepresentante(Consultas con)
        {
            InitializeComponent();
            this.con = con;
            rect.Width = grid.Width;

            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;
        }

        private void Docente_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModDocentes(con);
            MFrame = null;
        }

        private void Requisitos_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModRequisitos(con);
            MFrame = null;
        }

        private void Repre_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModRepresentante(con);
            MFrame = null;
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
        }
    }
}
