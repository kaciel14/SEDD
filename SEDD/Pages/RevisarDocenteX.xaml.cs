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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SEDD.Scripts;

namespace SEDD.Pages
{
    /// <summary>
    /// Lógica de interacción para RevisarDocenteX.xaml
    /// </summary>
    public partial class RevisarDocenteX : Page
    {
        Docente d;
        Consultas con;
        Class1 c;
        List<String> reqs;
        //Frame RFrame;
        Char codigo = 'A';
        String codigoComp;
        int puntos = 1;


        public RevisarDocenteX(Consultas con, Class1 c)
        {
            this.c = c;
            this.con = con;
            //this.RFrame = f;
            InitializeComponent();
            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            punto.Content = puntos;

            codigoComp = codigo + "%";
            reqs = con.consultaRequisitosPrevios();
            listaRequsitos();
            
        }

        public RevisarDocenteX(Consultas con, Docente x, Class1 c)
        {
            this.c = c;
            this.con = con;
            //this.RFrame = f;
            InitializeComponent();
            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            punto.Content = puntos;

            this.d = x;
            Nombretxt.Content = x.getNombre();
            codigoComp = codigo + "%";
            reqs = con.consultaRequisitosPrevios();
            listaRequsitos();

            String s = con.consultaMaxReq();
            char[] cArr = s.ToCharArray();
            char character = cArr[0];
            if (character <= 'B')
            {
                sig.Visibility = Visibility.Hidden;
            }

            at.Visibility = Visibility.Hidden;
        }

        private void listaRequsitos()
        {
            RequisitoSingle r = null;

            int b = 1;
            foreach (String s in reqs)
            {
                if (b.Equals(1))
                {
                    stackChild.Children.Add(r = new RequisitoSingle(s, con, RFrame, d, c));
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

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            RFrame.Content = new LoginPage();
            RFrame = null;
        }

        public void setDocente(Docente x)
        {
            this.d = x;
        }

        private void back_btn(object sender, RoutedEventArgs e)
        {
            RFrame.Content = new MainRevisor(con, c);
            RFrame = null;
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
                stackChild.Children.Clear();
                listaRequsitos();

                if (codigo == c)
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
                stackChild.Children.Clear();
                listaRequsitos();

                if (codigo == 'A')
                {
                    at.Visibility = Visibility.Hidden;
                }

                sig.Visibility = Visibility.Visible;
            }
        }

        public class RequisitoSingle : StackPanel
        {
            String id;
            String desc;
            public String obligatorio;

            Docente d;

            TextBlock nombre;
            Button revisar;

            Consultas con;
            Frame f;

            Class1 c;
            Frame frame;

            Label estado;

            public RequisitoSingle(String id, Consultas con, Frame f, Docente d, Class1 c)
            {
                this.id = id;
                //this.desc = desc;
                this.d = d;
                this.c = c;

                this.con = con;
                this.f = f;

                frame = f;

                this.Margin = new Thickness(0, 0, 0, 20);

                this.HorizontalAlignment = HorizontalAlignment.Left;

                this.Orientation = Orientation.Horizontal;

                nombre = new TextBlock();
                revisar = new Button();
                estado = new Label();

                //nombre.Content = desc;
                revisar.Content = "Ver";
                estado.Content = "Sin subir";

                revisar.Click += new RoutedEventHandler(revisarClick);

                this.Children.Add(nombre);
                this.Children.Add(revisar);
                this.Children.Add(estado);

                nombre.Margin = new Thickness(0, 0, 0, 0);
                nombre.Width = 60 * 9;
                nombre.VerticalAlignment = VerticalAlignment.Center;
                revisar.Margin = new Thickness(40, 0, 0, 0);
                estado.Margin = new Thickness(60, 0, 0, 0);
                estado.VerticalAlignment = VerticalAlignment.Center;

            }

            public void setDesc(String desc)
            {
                desc = desc.Replace('\n', ' ');
                this.desc = desc;
                nombre.Text = desc;
                int len = 80;
                int max = desc.Length;

                while (max > len)
                {
                    int i = len;
                    while (i < max && !desc.ElementAt(i).Equals(' '))
                    {
                        i++;
                    }
                    if (i < max && desc.ElementAt(i).Equals(' '))
                    {
                        desc = desc.Insert(i, "\n");
                        desc = desc.Remove(i + 1, 1);
                        nombre.Height = nombre.Height + 20;
                    }

                    len = len + 80;

                    nombre.Text = desc;

                    //nombre.Height = nombre.Height * 2 + 5;

                }

                String edo = con.consultaEstado(id, d.RFC);
                estado.Content = edo;

                if (edo.Equals("Denegado"))
                {
                    estado.Foreground = System.Windows.Media.Brushes.Red;
                }
                else if (edo.Equals("En espera"))
                {
                    estado.Foreground = System.Windows.Media.Brushes.Orange;
                }
                else if (edo.Equals("Aceptado"))
                {
                    estado.Foreground = System.Windows.Media.Brushes.Green;
                }


                if (obligatorio.Equals("True"))
                {
                    Run run = new Run("*");
                    run.FontSize = 18;
                    run.FontWeight = FontWeights.Bold;
                    run.Foreground = System.Windows.Media.Brushes.Red;

                    nombre.Inlines.Add(run);
                }
            }

            public void revisarClick(object sender, RoutedEventArgs e)
            {
                if (!estado.Content.Equals("Por subir"))
                {
                    f.Content = new VerDocumento(con, d, c, id);
                    f = null;
                }
                
            }


        }

        private void Lista_btn(object sender, RoutedEventArgs e)
        {
            RFrame.Content = new MainRevisor(con, c);
            RFrame = null;
        }

        private void Menu_btn(object sender, RoutedEventArgs e)
        {
            RFrame.Content = new ModePage(con);
            RFrame = null;
        }
    }
}
