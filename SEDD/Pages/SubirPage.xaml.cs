using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing.Imaging;
using System.Drawing;
//using System.Windows.Forms;
//using Label = System.Windows.Controls.Label;
//using Button = System.Windows.Controls.Button;
using Microsoft.Win32;

namespace SEDD.Pages
{
    /// <summary>
    /// Lógica de interacción para SubirPage.xaml
    /// </summary>
    public partial class SubirPage : Page
    {
        Consultas con;
        List<String> reqs;
        public SubirPage(Consultas con)
        {
            this.con = con;
            InitializeComponent();
            rect.Width = grid.Width;
            reqs = con.consultaRequisitosPrevios();
            listaRequsitos();
            //this.MFrame = f;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            if (!con.consultaRol())
            {
                menubtn.Visibility = Visibility.Hidden;
                datosbtn.Margin = new Thickness(370, 5, 0, 0);
                reqbtn.Margin = new Thickness(570, 5, 0, 0);
                reqprbtn.Margin = new Thickness(770, 5, 0, 0);
            }
        }
        private void BacktBtn_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new Page1(con);
            MFrame = null;
        }

        private void Req_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new SubirReq(con);
            MFrame = null;
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new Page1(con);
            MFrame = null;
        }

        private void listaRequsitos()
        {
            Requisito r = null;
            int b = 1;
            foreach (String s in reqs)
            {
                if (b.Equals(1))
                {
                    stack.Children.Add(r = new Requisito(s,con,MFrame));
                    b = 2;
                }
                else if(b.Equals(0))
                {
                    r.setDesc(s);
                    r.Margin = new Thickness(0, 0, 0, 40);
                    b = 1;
                }else if (b.Equals(2))
                {
                    r.obligatorio = s;
                    b = 0;
                }
            }
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
        }

        private void Req_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new SubirReq(con);
            MFrame = null;
        }

        private void Menu_btn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new ModePage(con);
            MFrame = null;
        }
    }

    public class Requisito : StackPanel
    {
        String id;
        String desc;
        public String obligatorio;
        TextBlock nombre;
        Button subir;
        Button ver;
        Button descargar;
        Button remplazar;
        Label estado;
        Consultas con;
        Frame frame;


        public Requisito(String id, Consultas con, Frame f)
        {
            this.id = id;
            //this.desc = desc;
            this.con = con;
            this.frame = f;

            this.Margin = new Thickness(0, 0, 0, 20);

            this.HorizontalAlignment = HorizontalAlignment.Left;

            this.Orientation = Orientation.Horizontal;

            nombre = new TextBlock();
            subir = new Button();
            ver = new Button();
            descargar = new Button();
            remplazar = new Button();
            estado = new Label();

            //nombre.Content = desc;
            subir.Content = "Subir";
            ver.Content = "Ver";
            descargar.Content = "Descargar";
            remplazar.Content = "Eliminar";
            estado.Content = "Sin subir";

            subir.Click += new RoutedEventHandler(subirClick);
            ver.Click += new RoutedEventHandler(verClick);
            remplazar.Click += new RoutedEventHandler(remplazarDoc);
            descargar.Click += new RoutedEventHandler(descargarDoc);

            nombre.Margin = new Thickness(50, 0, 10, 0);
            nombre.Width = 60 * 9;
            nombre.VerticalAlignment = VerticalAlignment.Center;
            subir.Margin = new Thickness(15, 0, 0, 0);
            ver.Margin = new Thickness(15, 0, 0, 0);
            descargar.Margin = new Thickness(15, 0, 0, 0);
            remplazar.Margin = new Thickness(15, 0, 0, 0);
            estado.Margin = new Thickness(15, 0, 0, 0);
            estado.VerticalAlignment = VerticalAlignment.Center;

            this.Children.Add(nombre);
            this.Children.Add(subir);
            this.Children.Add(ver);
            this.Children.Add(descargar);
            this.Children.Add(remplazar);
            this.Children.Add(estado);
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
                while (i<max && !desc.ElementAt(i).Equals(' '))
                {
                    i++;
                }
                if(i < max && desc.ElementAt(i).Equals(' '))
                {
                    desc = desc.Insert(i, "\n");
                    desc = desc.Remove(i + 1, 1);
                    nombre.Height = nombre.Height + 20;  
                }

                len = len + 80;

                nombre.Text = desc;

                //nombre.Height = nombre.Height * 2 + 5;

            }
            
            String edo = con.consultaEstado(id,"");
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

        public void subirClick(object sender, RoutedEventArgs e)
        {
            /*System.Drawing.Image i = System.Drawing.Image.FromFile("E:\\docPrueba.jpg");


            Documento d = new Documento(i);

            con.nuevoDoc(d, id);*/

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Selecione una imagen";
            dialog.Filter = "*.jpg|*.jpg";
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dialog.ShowDialog() == true)
            {
                System.Drawing.Image i = System.Drawing.Image.FromFile(dialog.FileName);
                Documento d = new Documento(i);

                if(d.fotoBytes != null)
                {
                    con.nuevoDoc(d, id);
                    estado.Content = con.consultaEstado(id,"");

                    if (estado.Content.Equals("Denegado"))
                    {
                        estado.Foreground = System.Windows.Media.Brushes.Red;
                    }
                    else if (estado.Content.Equals("En espera"))
                    {
                        estado.Foreground = System.Windows.Media.Brushes.Orange;
                    }
                    else if (estado.Content.Equals("Aceptado"))
                    {
                        estado.Foreground = System.Windows.Media.Brushes.Green;
                    }
                }
                else
                {
                    MessageBox.Show("No es posible cargar imagenes mayores a 500 kb", "Tamaño incorrecto");
                }
                

            }
        }

        public void verClick(object sender, RoutedEventArgs e)
        {
            if (!estado.Content.Equals("Por subir"))
            {
                frame.Content = new VerDocumento(con, this.id);
                frame = null;
            }
            
        }

        public void remplazarDoc(object sender, RoutedEventArgs e)
        {
            if (!estado.Content.Equals("Por subir"))
            {
                con.eliminarDoc(this.id);
                estado.Content = "Por subir";
                estado.Foreground = System.Windows.Media.Brushes.Black;
            }

        }

        public void descargarDoc(object sender, RoutedEventArgs e)
        {
            List<Documento> listaDocs;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Crear carpeta para guardar";
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.FileName = "Nueva Carpeta";

            if (dialog.ShowDialog() == true)
            {
                if (Directory.Exists(dialog.FileName))
                {
                    MessageBox.Show("La carpeta ya existe");
                }
                else
                {
                    Directory.CreateDirectory(dialog.FileName);

                    listaDocs = con.consultaImg(id);
                    int n = 0;
                    foreach (Documento d in listaDocs)
                    {
                        BitmapImage bim = new BitmapImage();
                        bim.BeginInit();
                        bim.CacheOption = BitmapCacheOption.OnLoad;
                        bim.StreamSource = Documento.ByteToImage(d.fotoBytes);
                        bim.EndInit();

                        System.Drawing.Image i = System.Drawing.Image.FromStream(bim.StreamSource);
                        i.Save(dialog.FileName + "\\"+ id + "_" + n + ".jpg");
                        n++;
                    }
                }




            }
        }
    }


}
