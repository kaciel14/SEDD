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
    /// Lógica de interacción para ModRepresentante.xaml
    /// </summary>
    public partial class ModRepresentante : Page
    {
        Consultas con;

        public ModRepresentante(Consultas con)
        {
            InitializeComponent();
            this.con = con;
            rect.Width = grid.Width;
            rfcBox.Text = con.RFC;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;
            nameBox.Text = con.ConsultaContraRepre();
        }

        private void actualiza(object sender, RoutedEventArgs e)
        {
            con.actualizaRepre(nameBox.Text);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new MainRepresentante(con);
            MFrame = null;
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
        }
    }
}
