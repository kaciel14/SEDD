using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para ModDocentes.xaml
    /// </summary>
    public partial class ModDocentes : Page
    {

        Consultas con;
        List<Docente> docentes = new List<Docente>();
        Docente sel;
        String aux;

        public ModDocentes(Consultas con)
        {
            InitializeComponent();
            this.con = con;

            docentes = con.getTabla();
            Console.WriteLine(docentes.Count);
            fillTable();

            rect.Width = grid.Width;
            departamentoLabel.Content = con.Departamento;
            departamentoLabel.FontWeight = FontWeights.Bold;
            departamentoLabel.Foreground = System.Windows.Media.Brushes.White;
            nameBox.TextWrapping = TextWrapping.NoWrap;
            rfcBox.IsEnabled = false;

        }

        public void fillTable()
        {
            table.ItemsSource = docentes;
        }


        private void actualiza(object sender, RoutedEventArgs e)
        {
            sel.RFC = rfcBox.Text;
            sel.Nombre = nameBox.Text;
            sel.Clave = numFicBox.Text;
            sel.Departamento = depBox.Text;
            sel.Correo = emailBox.Text;
            sel.Puesto = ((ComboBoxItem)roleChooser.SelectedItem).Content.ToString();
            con.actualizarDocente(sel);
            docentes = con.getTabla();
            table.ItemsSource = docentes;
        }


        private void select(object sender, EventArgs e)
        {
            try
            {
                sel = (Docente)table.CurrentCell.Item;
                rfcBox.Text = sel.RFC;
                nameBox.Text = sel.Nombre;
                numFicBox.Text = sel.Clave;
                depBox.Text = sel.Departamento;
                emailBox.Text = sel.Correo;
                roleChooser.SelectedItem = (ComboBoxItem)roleChooser.FindName(sel.Puesto);
            }
            catch (Exception ex)
            {

            } 
        }

        private void addDocente(object sender, RoutedEventArgs e)
        {
            table.IsEnabled = false;
            rfcBox.IsEnabled = true;
            rfcBox.Text = "";
            nameBox.Text = "";
            numFicBox.Text = "";
            depBox.Text = "";
            emailBox.Text = "";
            roleChooser.SelectedItem = null;


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

        private void eliminarDocente(object sender, RoutedEventArgs e)
        {
            con.eliminarDocente(sel);
            docentes = con.getTabla();
            Console.WriteLine(docentes.Count);
            fillTable();
            rfcBox.Text = "";
            nameBox.Text = "";
            numFicBox.Text = "";
            depBox.Text = "";
            emailBox.Text = "";
            roleChooser.SelectedItem = null;
        }

        private void nuevoDocente(object sender, RoutedEventArgs e)
        {

            sel = new Docente(nameBox.Text, rfcBox.Text, depBox.Text, emailBox.Text, numFicBox.Text,
                ((ComboBoxItem)roleChooser.SelectedItem).Content.ToString());

            con.nuevoDocente(sel);

            table.IsEnabled = true;
            rfcBox.IsEnabled = false;

            updateBtn.IsEnabled = true;
            addBtn.IsEnabled = true;
            updateBtn.Visibility = Visibility.Visible;
            addBtn.Visibility = Visibility.Visible;

            add2Btn.Visibility = Visibility.Hidden;
            add2Btn.IsEnabled = false;
            cancelBtn.Visibility = Visibility.Hidden;
            cancelBtn.IsEnabled = false;

            docentes = con.getTabla();
            Console.WriteLine(docentes.Count);
            fillTable();

            rfcBox.Text = "";
            nameBox.Text = "";
            numFicBox.Text = "";
            depBox.Text = "";
            emailBox.Text = "";
            roleChooser.SelectedItem = null;
        }

        private void CancelBtn(object sender, RoutedEventArgs e)
        {
            table.IsEnabled = true;
            rfcBox.IsEnabled = false;

            updateBtn.IsEnabled = true;
            addBtn.IsEnabled = true;
            updateBtn.Visibility = Visibility.Visible;
            addBtn.Visibility = Visibility.Visible;

            add2Btn.Visibility = Visibility.Hidden;
            add2Btn.IsEnabled = false;
            cancelBtn.Visibility = Visibility.Hidden;
            cancelBtn.IsEnabled = false;
            rfcBox.Text = "";
            nameBox.Text = "";
            numFicBox.Text = "";
            depBox.Text = "";
            emailBox.Text = "";
            roleChooser.SelectedItem = null;
        }
    }



}
