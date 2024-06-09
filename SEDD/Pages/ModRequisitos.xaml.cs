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
    /// Lógica de interacción para ModRequisitos.xaml
    /// </summary>
    public partial class ModRequisitos : Page
    {

        Consultas con;
        List<Requisitos> reqs = new List<Requisitos>();
        Requisitos sel;
        String selOriginalId;

        public ModRequisitos(Consultas con)
        {
            InitializeComponent();
            this.con = con;

            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;

            descBox.Height = 150;
            descBox.TextWrapping = TextWrapping.Wrap;

            reqs = con.getListaRequisitos();
            Console.WriteLine(reqs.Count);
            fillTable();

            rect.Width = grid.Width;
            descBox.TextWrapping = TextWrapping.NoWrap;
        }

        public void fillTable()
        {
            foreach(Requisitos r in reqs)
            {
                r.Descipcion = formatoTexto(r);
            }

            table.ItemsSource = reqs;
        }

        private void actualiza(object sender, RoutedEventArgs e)
        {
            sel.Code = idBox.Text;
            sel.Descipcion = descBox.Text;
            sel.Obligatorio = obBox.Text;
 
            con.actualizarRequisito(sel, selOriginalId);
            reqs = con.getListaRequisitos();
            table.ItemsSource = reqs;
        }


        private void select(object sender, EventArgs e)
        {
            try
            {
                sel = (Requisitos)table.CurrentCell.Item;
                idBox.Text = sel.Code;
                selOriginalId = sel.Code;
                dateBox.Text = sel.Fecha;
                obBox.Text = sel.Obligatorio;
                descBox.Text = sel.Descipcion;
                descBox.Height = 150;
                descBox.TextWrapping = TextWrapping.Wrap;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void addRequisito(object sender, RoutedEventArgs e)
        {
            table.IsEnabled = false;
            idBox.Text = "";
            descBox.Text = "";
            dateBox.Text = "";
            obBox.Text = "";


            updateBtn.IsEnabled = false;
            addBtn.IsEnabled = false;
            updateBtn.Visibility = Visibility.Hidden;
            addBtn.Visibility = Visibility.Hidden;

            add2Btn.Visibility = Visibility.Visible;
            add2Btn.IsEnabled = true;
            cancelBtn.Visibility = Visibility.Visible;
            cancelBtn.IsEnabled = true;

        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new LoginPage();
            MFrame = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MFrame.Content = new MainRepresentante(con);
            MFrame = null;
        }

        private void nuevoReq(object sender, RoutedEventArgs e)
        {
            sel = new Requisitos(idBox.Text, descBox.Text, null, obBox.Text);

            con.nuevoRequisito(sel);

            table.IsEnabled = true;

            updateBtn.IsEnabled = true;
            addBtn.IsEnabled = true;
            updateBtn.Visibility = Visibility.Visible;
            addBtn.Visibility = Visibility.Visible;

            add2Btn.Visibility = Visibility.Hidden;
            add2Btn.IsEnabled = false;
            cancelBtn.Visibility = Visibility.Hidden;
            cancelBtn.IsEnabled = false;

            reqs = con.getListaRequisitos();
            Console.WriteLine(reqs.Count);
            fillTable();


            idBox.Text = "";
            descBox.Text = "";
            dateBox.Text = "";
            obBox.Text = "";
        }

        private void CancelBtn(object sender, RoutedEventArgs e)
        {
            table.IsEnabled = true;

            updateBtn.IsEnabled = true;
            addBtn.IsEnabled = true;
            updateBtn.Visibility = Visibility.Visible;
            addBtn.Visibility = Visibility.Visible;

            add2Btn.Visibility = Visibility.Hidden;
            add2Btn.IsEnabled = false;
            cancelBtn.Visibility = Visibility.Hidden;
            cancelBtn.IsEnabled = false;

            idBox.Text = "";
            descBox.Text = "";
            dateBox.Text = "";
            obBox.Text = "";
        }

        private void EliminarRequisito(object sender, RoutedEventArgs e)
        {
            con.eliminarRequisito(sel);
            reqs = con.getListaRequisitos();
            Console.WriteLine(reqs.Count);
            fillTable();
            idBox.Text = "";
            descBox.Text = "";
            dateBox.Text = "";
            obBox.Text = "";
        }

        public String formatoTexto(Requisitos r)
        {
            String desc = r.Descipcion;
            desc = desc.Replace('\n', ' ');
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
                }

                len = len + 80;

                //nombre.Height = nombre.Height * 2 + 5;

            }
            return desc;
        }
    }
}
