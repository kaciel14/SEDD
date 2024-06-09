using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SEDD.Scripts
{
    public class Class1
    {
        public List<Docente> docentes;
        Consultas db;
        Docente main;

        public Class1(Consultas con)
        {
            db = con;
            docentes = new List<Docente>();
            llenarLista();
            //pruebas(35);
        }

        public int getDocentes()
        {
            return docentes.Count;
        }

        public void creaDocente(String s)
        {
            //docentes.Add(new Docente(s));
        }

        public void llenarLista()
        {
            docentes = db.getLista();
        }

        
    }

    
}
