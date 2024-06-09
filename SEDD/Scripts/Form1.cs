using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEDD.Scripts
{
    public partial class Comentarios : Form
    {
        Consultas con;
        String id;
        String rfc;
        public Comentarios(Consultas con, String idreq, String rfc)
        {
            InitializeComponent();
            this.con = con;
            id = idreq;
            this.rfc = rfc;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Comentarios_Load(object sender, EventArgs e)
        {

        }

        private void Listo_Btn(object sender, EventArgs e)
        {
            con.setEstadoDocumento("Denegado", id, rfc);
            con.insertarComentario(textBox1.Text, id, rfc);
            this.Close();
            this.Dispose();
        }
    }
}
