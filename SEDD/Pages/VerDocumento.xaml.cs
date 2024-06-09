using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using SEDD.Scripts;

namespace SEDD.Pages
{
    /// <summary>
    /// Lógica de interacción para VerDocumento.xaml
    /// </summary>
    public partial class VerDocumento : Page
    {

        Consultas con;
        List<Documento> listaDocs;
        int index;
        String idreq;
        bool revisor = false;
        Class1 c;
        Docente d;
        bool b = false;
        String rfc;

        //Frame MFrame;

        public VerDocumento(Consultas con, String id)
        {
            this.con = con;
            InitializeComponent();
            idreq = id;

            listaDocs = con.consultaImg(id);

            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            cambiarImg();
            //this.MFrame = f;

            String desc = con.consultaDescripcion(id);

            desc = desc.Replace('\n', ' ');
            int len = 20;
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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }


            LabelDesc.Content = "Descripción:\n" + desc;

            LabelEstado.Content = con.consultaEstado(id, "");


            if (LabelEstado.Content.Equals("Denegado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Red;
            }
            else if (LabelEstado.Content.Equals("En espera"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Orange;
            }
            else if (LabelEstado.Content.Equals("Aceptado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Green;
            }

            desc = con.consultaComentario(id, "");

            desc = desc.Replace('\n', ' ');

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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }

            LabelComms.Content = "Comentarios:" + "\n" +desc;
        }

        public VerDocumento(Consultas con, Docente d, Class1 c, String id)
        {
            this.con = con;
            InitializeComponent();

            idreq = id;
            this.d = d;
            this.rfc = d.RFC;
            this.c = c;

            listaDocs = con.consultaImg(id, d.getRFC());

            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            modeRevisor();
            cambiarImg();
            //this.MFrame = f;

            String desc = con.consultaDescripcion(id);

            desc = desc.Replace('\n', ' ');
            int len = 20;
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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }


            LabelDesc.Content = "Descripción:\n" + desc;

            LabelEstado.Content = con.consultaEstado(id, d.RFC);
            

            if (LabelEstado.Content.Equals("Denegado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Red;
            }
            else if (LabelEstado.Content.Equals("En espera"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Orange;
            }
            else if (LabelEstado.Content.Equals("Aceptado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Green;
            }

            desc = con.consultaComentario(id, d.RFC);

            desc = desc.Replace('\n', ' ');

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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }

            LabelComms.Content = "Comentarios:" + "\n" + desc;
        }

        public VerDocumento(Consultas con, String d, Class1 c, String id)
        {
            this.rfc = d;
            this.d = new Docente(d, "");
            this.con = con;
            InitializeComponent();

            idreq = id;
            this.c = c;
            b = true;

            listaDocs = con.consultaImg(id, d);

            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            modeRevisor();
            cambiarImg();
            //this.MFrame = f;

            String desc = con.consultaDescripcion(id);

            desc = desc.Replace('\n', ' ');
            int len = 20;
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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }


            LabelDesc.Content = "Descripción:\n" + desc;

            LabelEstado.Content = con.consultaEstado(id, d);


            if (LabelEstado.Content.Equals("Denegado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Red;
            }
            else if (LabelEstado.Content.Equals("En espera"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Orange;
            }
            else if (LabelEstado.Content.Equals("Aceptado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Green;
            }

            desc = con.consultaComentario(id, d);

            desc = desc.Replace('\n', ' ');

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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }

            LabelComms.Content = "Comentarios:" + "\n" + desc;
        }

        public void cambiarImg()
        {

            if (index < listaDocs.Count-1)
            {
                rightbtn.Visibility = Visibility.Visible;
            }
            else
            {
                rightbtn.Visibility = Visibility.Hidden;
            }

            if(index > 0)
            {
                leftbtn.Visibility = Visibility.Visible;
            }
            else
            {
                leftbtn.Visibility = Visibility.Hidden;
            }
            BitmapImage bim = new BitmapImage();
            bim.BeginInit();
            bim.CacheOption = BitmapCacheOption.OnLoad;
            bim.StreamSource = Documento.ByteToImage(listaDocs[index].fotoBytes);
            bim.EndInit();

            rect.Width = grid.Width;
            imgFrame.Source = bim;
            imgFrame.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            imgFrame.Stretch = Stretch.Fill;
        }

        public void modeRevisor()
        {
            revisor = true;
            aceptarbtn.Visibility = Visibility.Visible;
            rechazarbtn.Visibility = Visibility.Visible;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
            
        }

        private void leftBtn_Click(object sender, RoutedEventArgs e)
        {
            if(index > 0)
            {
                index--;
                cambiarImg();
            }
            
        }

        private void rightBtn_Click(object sender, RoutedEventArgs e)
        {
            if(index < listaDocs.Count-1)
            {
                index++;
                cambiarImg();
            }
            
        }

        private void aceptarBtn_Click(object sender, RoutedEventArgs e)
        {
            con.setEstadoDocumento("Aceptado", idreq, rfc);

            LabelEstado.Content = con.consultaEstado(this.idreq, d.RFC);


            if (LabelEstado.Content.Equals("Denegado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Red;
            }
            else if (LabelEstado.Content.Equals("En espera"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Orange;
            }
            else if (LabelEstado.Content.Equals("Aceptado"))
            {
                LabelEstado.Foreground = System.Windows.Media.Brushes.Green;
            }

            con.insertarComentario("", idreq, d.RFC);
            LabelComms.Content ="Comentarios: \n" + con.consultaComentario(idreq, d.RFC);
        }

        private void rechazarBtn_Click(object sender, RoutedEventArgs e)
        {
            Form form = new Form();

            try
            {
                using (Comentarios comm = new Comentarios(con, idreq, rfc))
                {
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Opacity = .70d;
                    form.BackColor = System.Drawing.Color.Black;
                    form.WindowState = FormWindowState.Normal;
                    //form.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width/2, Screen.PrimaryScreen.Bounds.Height/2);
                    //form.Show();

                    comm.Owner = form;
                    comm.StartPosition = FormStartPosition.CenterScreen;
                    comm.Disposed += new EventHandler(cerrado);
                    comm.ShowDialog();
                    

                }

                LabelEstado.Content = con.consultaEstado(this.idreq, d.RFC);


                if (LabelEstado.Content.Equals("Denegado"))
                {
                    LabelEstado.Foreground = System.Windows.Media.Brushes.Red;
                }
                else if (LabelEstado.Content.Equals("En espera"))
                {
                    LabelEstado.Foreground = System.Windows.Media.Brushes.Orange;
                }
                else if (LabelEstado.Content.Equals("Aceptado"))
                {
                    LabelEstado.Foreground = System.Windows.Media.Brushes.Green;
                }
            }

            catch (Exception ex)
            {

            }

            //con.setEstadoDocumento("Denegado", idreq, rfc);
        }

        public void cerrado(object sender, EventArgs e)
        {
            String desc;
            desc = con.consultaComentario(this.idreq, d.RFC);
            int len = 20;
            int max = desc.Length;
            desc = desc.Replace('\n', ' ');

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
                }

                len = len + 20;

                //nombre.Height = nombre.Height * 2 + 5;

            }

            LabelComms.Content = "Comentarios:" + "\n" + desc;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (revisor)
            {
                if (b)
                {
                    MFrame.Content = new MainRevisor(con, c);
                    MFrame = null;
                }
                else
                {
                    MFrame.Content = new RevisarDocenteX(con, d, c);
                    MFrame = null;
                }

            }
            else
            {
                MFrame.Content = new SubirPage(con);
                MFrame = null;
            }
        }
    }
}
