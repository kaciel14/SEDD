using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace SEDD.Scripts
{
    public class Consultas
    {
        MySqlConnection db;
        String lastRFC;
        String lastClave;
        String departamento;
        public Docente d;

        public Consultas(MySqlConnection con)
        {
            db = con;
        }

        public void setUsuario()
        {
            String nombre = "", clave = "", correo = "", depa = "", puesto = "";

            try
            {
                String data = "";
                MySqlDataReader reader = null;
                //MySqlCommand consulta = new MySqlCommand("select Nombre, ClaveArea from personal where RFC = \"" + lastRFC + "\";", db);
                MySqlCommand consulta = new MySqlCommand("call consulta_personalRFC(@rfc);", db);
                consulta.Parameters.AddWithValue("@rfc", lastRFC);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    nombre = reader.GetString(0);
                    depa = reader.GetString(1);
                    correo = reader.GetString(2);
                    clave = reader.GetString(3);
                    //puesto = reader.GetString(4);
                }

                reader.Close();
                Console.WriteLine(data);

                if (nombre != "")
                {
                    lastClave = clave;
                    d = new Docente(nombre, lastRFC, depa, correo, lastClave, puesto);
                    setDepartamento();
                }


            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Usuario no encontrado");
            }
        }

        public int BuscarUsuario(String rfc, String pass)
        {
            String data = "";
            try
            {
                MySqlDataReader reader = null;
                //MySqlCommand consulta = new MySqlCommand("select NumeroFicha from personal where RFC = \""+rfc+"\";",db);
                MySqlCommand consulta = new MySqlCommand("call validar(@rfc);", db);
                consulta.Parameters.AddWithValue("@rfc", rfc);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    data += reader.GetString(0);
                }
                reader.Close();

                if (data != "")
                {
                    if (data.Equals(pass))
                    {
                        lastRFC = rfc;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    consulta = new MySqlCommand("select contraseña, area from representante where usuario = @user;", db);
                    consulta.Parameters.AddWithValue("@user", rfc);
                    reader = consulta.ExecuteReader();
                    String area = "";

                    while (reader.Read())
                    {
                        data += reader.GetString(0);
                        area += reader.GetString(1);
                    }
                    reader.Close();

                    if (data != "")
                    {
                        if (data.Equals(pass))
                        {
                            lastRFC = rfc;
                            lastClave = area;
                            setDepartamento();
                            return 2;
                        }
                        else
                        {
                            return 0;
                        }

                    }
                    else
                    {
                        return 0;
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public void setDepartamento()
        {

            try
            {
                String data = "";
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand("select Nombre from departamentos where ClaveArea = @clave", db);
                consulta.Parameters.AddWithValue("@clave", lastClave);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    departamento = reader.GetString(0);
                }

                reader.Close();
                Console.WriteLine(data);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Usuario no encontrado");
            }
        }

        /*public Docente ObtenerUsuario()
        {
            String nombre = "", clave="";

            try
            {
                String data = "";
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand("select Nombre, ClaveArea from personal where RFC = \"" + lastRFC + "\";", db);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    nombre = reader.GetString(0);
                    clave = reader.GetString(1);
                }

                reader.Close();
                Console.WriteLine(data);

                if (nombre != "")
                {
                    lastClave = clave;
                    return null;//new Docente(nombre, lastRFC, clave);
                }
                else
                {
                    return null;
                }


            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Usuario no encontrado");
                return null;
            }
        }*/

        public List<Docente> getLista()
        {

            try
            {
                List<Docente> lista = new List<Docente>();
                String data = "";
                MySqlDataReader reader = null;
                //MySqlCommand consulta = new MySqlCommand("select Nombre from personal where ClaveArea = \"" + lastClave + "\";", db);
                MySqlCommand consulta = new MySqlCommand("call consulta_personalClave(@clave);", db);
                consulta.Parameters.AddWithValue("@clave", lastClave);
                reader = consulta.ExecuteReader();
                Docente x;

                while (reader.Read())
                {
                    data = reader.GetString(0);
                    lista.Add(x = new Docente(data));
                    x.setRFC(reader.GetString(1));
                }

                reader.Close();
                Console.WriteLine(data);

                return lista;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Usuario no encontrado");
                return null;
            }
        }

        public List<Docente> getTabla()
        {

            try
            {
                List<Docente> lista = new List<Docente>();
                String nombre = "", rfc = "", correo = "", numFicha = "", puesto = "";
                MySqlDataReader reader = null;
                //MySqlCommand consulta = new MySqlCommand("select Nombre from personal where ClaveArea = \"" + lastClave + "\";", db);
                MySqlCommand consulta = new MySqlCommand("call consulta_personalClave(@clave);", db);
                consulta.Parameters.AddWithValue("@clave", lastClave);
                reader = consulta.ExecuteReader();
                Docente x;

                while (reader.Read())
                {
                    nombre = reader.GetString(0);
                    rfc = reader.GetString(1);
                    numFicha = reader.GetString(3);
                    correo = reader.GetString(4);
                    puesto = reader.GetString(5);
                    lista.Add(x = new Docente(nombre, rfc, lastClave, correo, numFicha, puesto));
                    //x.setRFC(reader.GetString(1));
                }

                reader.Close();
                //Console.WriteLine(data);

                return lista;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Usuario no encontrado");
                return null;
            }
        }

        public String getClave()
        {
            return lastClave;
        }

        public String Departamento { get => this.departamento; }


        public void nuevoDoc(Documento doc, String id)
        {
            try
            {
                String cadena = "insert into documentos (F_subida, Archivo, imagen, requisito, docente) values (now(), 'doc2', @img, @req, @docente );";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@img", doc.fotoBytes);
                consulta.Parameters.AddWithValue("@req", id);
                consulta.Parameters.AddWithValue("@docente", d.getRFC());
                consulta.ExecuteNonQuery();

                cadena = "update personal set UltimoDocumento = (select F_subida from documentos where requisito = @req and docente = @rfc) where RFC = @rfc;";
                consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@req", id);
                consulta.Parameters.AddWithValue("@rfc", d.getRFC());
                consulta.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public List<Documento> consultaImg(String idReq)
        {
            List<Documento> lista = new List<Documento>();
            //byte[] imagen = null;
            try
            {
                String cadena = "select imagen from documentos where requisito = @req and docente = @rfc;";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@req", idReq);
                consulta.Parameters.AddWithValue("@rfc", d.getRFC());
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    //imagen = (byte[])reader.GetValue(0);
                    lista.Add(new Documento((byte[])reader.GetValue(0)));
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

            return lista;
        }

        public List<Documento> consultaImg(String idReq, String rfc)
        {
            List<Documento> lista = new List<Documento>();
            //byte[] imagen = null;
            try
            {
                String cadena = "select imagen from documentos where requisito = @req and docente = @rfc;";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@req", idReq);
                consulta.Parameters.AddWithValue("@rfc", rfc);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    //imagen = (byte[])reader.GetValue(0);
                    lista.Add(new Documento((byte[])reader.GetValue(0)));
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

            return lista;
        }

        public List<String> consultaRequisitosPrevios()
        {
            List<String> reqs = new List<String>();
            try
            {
                String cadena = "select id, descripcion, anio, obligatorio from requisitos where id like 'A%';";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    reqs.Add(reader.GetString(0));
                    reqs.Add(reader.GetString(3));
                    reqs.Add(reader.GetString(1));
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

            return reqs;
        }

        public List<String> consultaRequisitos(String code)
        {
            List<String> reqs = new List<String>();
            try
            {
                String cadena = "select id, descripcion, anio, obligatorio from requisitos where id like @code;";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@code", code);
                Console.WriteLine(consulta.CommandText);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    reqs.Add(reader.GetString(0));
                    reqs.Add(reader.GetString(3));
                    reqs.Add(reader.GetString(1));
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

            return reqs;
        }

        public bool consultaRol()
        {
            String data = "";
            try
            {
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand("select Puesto from personal where RFC = \"" + lastRFC + "\";", db);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    data += reader.GetString(0);
                }
                reader.Close();

                if (data.Equals("Revisor"))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void setEstadoDocumento(String edo, String idReq, String rfc)
        {
            try
            {
                MySqlCommand consulta = new MySqlCommand("update documentos set Validacion = @val where requisito = @req and docente = @rfc;", db);
                consulta.Parameters.AddWithValue("@val", edo);
                consulta.Parameters.AddWithValue("@req", idReq);
                consulta.Parameters.AddWithValue("@rfc", rfc);
                consulta.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        //OJO: Tomar como ejemplo la declaración del reader fuera del try para los demás métodos también.
        public String consultaEstado(String req, String docente)
        {
            String data = "Por subir";
            MySqlDataReader reader = null;
            try
            {

                MySqlCommand consulta = new MySqlCommand("select Validacion from documentos where docente = @rfc and requisito = @req;", db);
                consulta.Parameters.AddWithValue("@req", req);
                if (docente.Equals(""))
                {
                    consulta.Parameters.AddWithValue("@rfc", d.getRFC());
                }
                else
                {
                    consulta.Parameters.AddWithValue("@rfc", docente);
                }
                reader = consulta.ExecuteReader();

                /*while (reader.Read())
                {
                    data += reader.GetValue(0);
                }*/
                if (reader.Read())
                {
                    data = "";
                    data += reader.GetValue(0);
                }

                reader.Close();

                if (data.Equals(""))
                {
                    return "En espera";
                }
                else
                {
                    return data;
                }

            }
            catch (MySqlException ex)
            {
                reader.Close();
                MessageBox.Show(ex.Message);
                return "Error";
            }
        }

        public String consultaMaxReq()
        {
            String max = "";
            try
            {
                String cadena = "select max(id) from requisitos";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                Console.WriteLine(consulta.CommandText);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    max = reader.GetString(0);
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

            return max;
        }

        public void actualizarDocente(Docente d)
        {

            try
            {
                String cadena = "update personal set Nombre = @name, ClaveArea = @area, NumeroFicha = @numfic," +
                    " Email = @email, Puesto = @puesto where RFC = @rfc";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@name", d.Nombre);
                consulta.Parameters.AddWithValue("@area", d.Departamento);
                consulta.Parameters.AddWithValue("@numfic", d.Clave);
                consulta.Parameters.AddWithValue("@email", d.Correo);
                consulta.Parameters.AddWithValue("@puesto", d.Puesto);
                consulta.Parameters.AddWithValue("@rfc", d.RFC);
                consulta.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void eliminarDoc(String idReq)
        {

            try
            {
                String cadena = "delete from documentos where requisito = @req and docente = @docente;";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@req", idReq);
                consulta.Parameters.AddWithValue("@docente", lastRFC);
                consulta.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public List<Requisitos> getListaRequisitos()
        {
            List<Requisitos> reqs = new List<Requisitos>();
            try
            {
                String cadena = "select id, descripcion, anio, obligatorio from requisitos;";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    reqs.Add(new Requisitos(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

            return reqs;

        }

        public void actualizarRequisito(Requisitos r, String clave)
        {

            try
            {
                String cadena = "update requisitos set id = @nClave, descripcion = @desc, obligatorio = @val where id = @id;";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@id", clave);
                consulta.Parameters.AddWithValue("@desc", r.Descipcion);
                consulta.Parameters.AddWithValue("@nClave", r.Code);
                consulta.Parameters.AddWithValue("@val", Boolean.Parse(r.Obligatorio));
                consulta.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public String RFC { get => this.lastRFC;}
        public String Clave { get => this.lastClave; }

        public String ConsultaContraRepre()
        {
            String resultado = "";
            try
            {
                String cadena = "select contraseña from representante where usuario = @user;";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@user", this.RFC);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }
            return resultado;
        }

        public void actualizaRepre(String contra)
        {
            try
            {
                String cadena = "update representante set usuario = @user, contraseña = @contra where usuario = @user";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@user", this.RFC);
                consulta.Parameters.AddWithValue("@contra", contra);
                consulta.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }
        }

        public List<String> consultaTodosDocumentos()
        {
            List<String> docs = new List<String>();
            try
            {
                // OJO: FILTRAR POR AREA DEL REVISOR
                String cadena = "select requisito, docente, F_subida, ifnull(Validacion, 'Por revisar') from documentos order by F_subida desc;";
                Console.WriteLine(cadena);
                MySqlDataReader reader = null;
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                reader = consulta.ExecuteReader();

                while (reader.Read())
                {
                    docs.Add(reader.GetString(0));
                    docs.Add(reader.GetString(1));
                    docs.Add(reader.GetString(2));
                    docs.Add(reader.GetString(3));
                }
                /*reader.Read();
                imagen = (byte[])reader.GetValue(0);
                */

                reader.Close();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }
            return docs;
        }

        public void eliminarDocente(Docente d)
        {
            try
            {
                String cadena = "delete from personal where rfc = @rfc";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@rfc", d.RFC);
                consulta.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void nuevoDocente(Docente d)
        {
            try
            {
                String cadena = "insert into personal values(@area, @rfc, @name, @numF, @correo, @rol, null)";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@name", d.Nombre);
                consulta.Parameters.AddWithValue("@area", d.Departamento);
                consulta.Parameters.AddWithValue("@numF", d.Clave);
                consulta.Parameters.AddWithValue("@correo", d.Correo);
                consulta.Parameters.AddWithValue("@rol", d.Puesto);
                consulta.Parameters.AddWithValue("@rfc", d.RFC);
                consulta.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void nuevoRequisito(Requisitos r)
        {
            try
            {
                String cadena = "insert into requisitos values(@id, @desc, curdate(), @ob)";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@id", r.Code);
                consulta.Parameters.AddWithValue("@desc", r.Descipcion);
                consulta.Parameters.AddWithValue("@ob", Boolean.Parse(r.Obligatorio));
                consulta.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void eliminarRequisito(Requisitos r)
        {
            try
            {
                String cadena = "delete from requisitos where id = @id";
                Console.WriteLine(cadena);
                MySqlCommand consulta = new MySqlCommand(cadena, db);
                consulta.Parameters.AddWithValue("@id", r.Code);
                consulta.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void insertarComentario(String coment, String id, String rfc)
        {
            try
            {
                MySqlCommand consulta = new MySqlCommand("update documentos set Comentarios = @com where requisito = @req and docente = @rfc;", db);
                consulta.Parameters.AddWithValue("@com", coment);
                consulta.Parameters.AddWithValue("@req", id);
                consulta.Parameters.AddWithValue("@rfc", rfc);
                consulta.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public String consultaDescripcion(String req)
        {
            String data = "";
            MySqlDataReader reader = null;
            try
            {

                MySqlCommand consulta = new MySqlCommand("select descripcion from requisitos where id = @req;", db);
                consulta.Parameters.AddWithValue("@req", req);
                reader = consulta.ExecuteReader();

                /*while (reader.Read())
                {
                    data += reader.GetValue(0);
                }*/
                if (reader.Read())
                {
                    data = "";
                    data += reader.GetValue(0);
                }

                reader.Close();
                

            }
            catch (MySqlException ex)
            {
                reader.Close();
                MessageBox.Show(ex.Message);
                return "Error";
            }

            return data;
        }

        public String consultaComentario(String req, String docente)
        {
            String data = "Sin Comentarios";
            MySqlDataReader reader = null;
            try
            {

                MySqlCommand consulta = new MySqlCommand("select Comentarios from documentos where docente = @rfc and requisito = @req;", db);
                consulta.Parameters.AddWithValue("@req", req);
                if (docente.Equals(""))
                {
                    consulta.Parameters.AddWithValue("@rfc", d.getRFC());
                }
                else
                {
                    consulta.Parameters.AddWithValue("@rfc", docente);
                }
                reader = consulta.ExecuteReader();

                /*while (reader.Read())
                {
                    data += reader.GetValue(0);
                }*/
                if (reader.Read())
                {
                    data = "";
                    data += reader.GetValue(0);
                }

                reader.Close();


            }
            catch (MySqlException ex)
            {
                reader.Close();
                MessageBox.Show(ex.Message);
                return "Error";
            }

            return data;
        }

    }
}
