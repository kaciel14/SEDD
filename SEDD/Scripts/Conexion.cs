using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace SEDD.Scripts
{
    class Conexion
    {
        MySqlConnection conexiondb;

        public Conexion()
        {
            String db = "server=200.188.143.162;port=3306;uid=adminedd;pwd=superuser;database=edd;";

            conexiondb = new MySqlConnection(db);

            try
            {
                conexiondb.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        public void CerrarConexion()
        {
            conexiondb.Close();
        }

        public MySqlConnection getConection()
        {
            return conexiondb;
        }
    }
}
