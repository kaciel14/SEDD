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
    /// Lógica de interacción para SubirReq.xaml
    /// </summary>
    /// 
    
    public partial class SubirReq : Page
    {

        Consultas con;
        List<String> reqs;
        Char codigo = 'B';
        String codigoComp;
        int puntos = 1;

        public SubirReq(Consultas con)
        {
            this.con = con;
            InitializeComponent();
            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            codigoComp = codigo + "%";
            reqs = con.consultaRequisitos(codigoComp);
            listaRequsitos();

            punto.Content = puntos;

            if (!con.consultaRol())
            {
                menubtn.Visibility = Visibility.Hidden;
                datosbtn.Margin = new Thickness(370, 5, 0, 0);
                reqbtn.Margin = new Thickness(570, 5, 0, 0);
                reqprbtn.Margin = new Thickness(770, 5, 0, 0);
            }

            String s = con.consultaMaxReq();
            char[] cArr = s.ToCharArray();
            char c = cArr[0];
            if (c <= 'B')
            {
                sig.Visibility = Visibility.Hidden;
            }

            at.Visibility = Visibility.Hidden;
        }

        private void listaRequsitos()
        {
            Requisito r = null;
            int b = 1;
            foreach (String s in reqs)
            {
                if (b.Equals(1))
                {
                    stack.Children.Add(r = new Requisito(s, con, MFrame));
                    b = 2;
                }
                else if (b.Equals(0))
                {
                    r.setDesc(s);
                    r.Margin = new Thickness(0, 0, 0, 40);
                    b = 1;
                }
                else if (b.Equals(2))
                {
                    r.obligatorio = s;
                    b = 0;
                }
            }
        }

        private void BacktBtn_Click(object sender, RoutedEventArgs e)
        { 
            MFrame.Content = new Page1(con);
            MFrame = null;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {

            MFrame.Content = new LoginPage();
            MFrame = null;
        }

        private void Subir_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new SubirPage(con);
            MFrame = null;
        }

        private void Info_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new Page1(con);
            MFrame = null;
        }

        private void Avanza_btn(object sender, RoutedEventArgs e)
        {
            String s = con.consultaMaxReq();
            char[] cArr = s.ToCharArray();
            char c = cArr[0];
            Console.WriteLine("Max:" + s);
            codigo++;

            if (codigo <= c)
            {
                puntos++;
                punto.Content = puntos;
                codigoComp = codigo + "%";
                reqs = con.consultaRequisitos(codigoComp);
                stack.Children.Clear();
                listaRequsitos();

                if(codigo == c)
                {
                    sig.Visibility = Visibility.Hidden;
                }
                at.Visibility = Visibility.Visible;

            }
            
        }

        private void Retrocede_btn(object sender, RoutedEventArgs e)
        {
            codigo--;

            if (codigo >= 'A')
            {
                puntos--;
                punto.Content = puntos;
                codigoComp = codigo + "%";
                reqs = con.consultaRequisitos(codigoComp);
                stack.Children.Clear();
                listaRequsitos();

                if (codigo == 'B')
                {
                    at.Visibility = Visibility.Hidden;
                }
                sig.Visibility = Visibility.Visible;

            }
        }

        private void Menu_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModePage(con);
            MFrame = null;
        }
    }

}
