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
    /// Lógica de interacción para MainRevisor.xaml
    /// </summary>
    public partial class MainRevisor : Page
    {
        public DocenteElemento d;
        public RequisitoElemento req;
        Consultas con;
        Docente docente;
        Class1 docentes;
        //Frame MFrame;

        public MainRevisor(Consultas con)
        {
            this.con = con;
            docentes = new Class1(con);
            InitializeComponent();
            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;
            //this.MFrame = f;
            int numDocentes = docentes.getDocentes();
            for (int i = 0; i< numDocentes; i++)
            {
                d = new DocenteElemento(docentes.docentes.ElementAt(i));
                d.Click += new RoutedEventHandler(docente_click);
                stack.Children.Add(d);
            }

        }

        public MainRevisor(Consultas con, Class1 c)
        {
            this.con = con;
            docentes = c;
            InitializeComponent();
            rect.Width = grid.Width;

            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            for (int i = 0; i < docentes.getDocentes(); i++)
            {
                d = new DocenteElemento(docentes.docentes.ElementAt(i));
                d.Click += new RoutedEventHandler(docente_click);
                stack.Children.Add(d);
            }

        }

        void docente_click(object sender, RoutedEventArgs e)
        {
            DocenteElemento de = (DocenteElemento)e.Source;
            MFrame.Content = new RevisarDocenteX(con, de.x, docentes);
            MFrame = null;
        }

        void documento_click(object sender, RoutedEventArgs e)
        {
            RequisitoElemento re = (RequisitoElemento)e.Source;
            MFrame.Content = new VerDocumento(con, re.rfc, docentes, re.idReq);
            MFrame = null;
        }



        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
        }

        public class DocenteElemento : Button
        {
            public EventHandler eh;
            public Docente x;
            public String nombre;

            public DocenteElemento(Docente x)
            {
                this.x = x;
                this.nombre = x.getNombre();
                this.AddText(nombre);
                this.Margin = new Thickness(0, 0, 0, 0);
                this.Height = 30;
            }
        }

        public class RequisitoElemento : Button
        {
            public EventHandler eh;
            public Requisitos x;
            public String idReq;
            public String rfc;
            public String fecha;
            public String val;
            String nombre;
            public Docente d;

            public RequisitoElemento(String id, String rfc, String fecha, String val)
            {
                this.idReq = id;
                this.rfc = rfc;
                this.fecha = fecha;
                this.val = val;

                nombre = idReq + ":\t\t" + rfc + "   \t==>\t\t" + fecha + ":\t\t" + val; 

                this.AddText(nombre);
                this.HorizontalContentAlignment = HorizontalAlignment.Left;
                this.Height = 30;

            }
        }

        private void VerRequisitos(object sender, RoutedEventArgs e)
        {
            stack.Children.Clear();
            List<String> reqs = con.consultaTodosDocumentos();
            RequisitoElemento re = null;
            int numReqs = reqs.Count;

            if(numReqs >= 4)
            {
                for (int i = 0; i < numReqs; i += 4)
                {
                    re = new RequisitoElemento(reqs[i], reqs[i + 1], reqs[i + 2], reqs[i + 3]);
                    re.Click += new RoutedEventHandler(documento_click);
                    stack.Children.Add(re);
                }
            }
            
        }

        private void VerDocentes(object sender, RoutedEventArgs e)
        {
            stack.Children.Clear();
            int numDocentes = docentes.getDocentes();
            for (int i = 0; i < numDocentes; i++)
            {
                d = new DocenteElemento(docentes.docentes.ElementAt(i));
                d.Click += new RoutedEventHandler(docente_click);
                stack.Children.Add(d);
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModePage(con);
            MFrame = null;
        }

        private void Menu_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModePage(con);
            MFrame = null;
        }
    }
}
