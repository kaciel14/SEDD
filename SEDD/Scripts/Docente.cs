using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDD.Scripts
{
    public class Docente
    {
        private String nombre;
        private String rfc;
        private String departamento, correo;
        private String clave;
        private String puesto;

        public Docente(String nombre, String rfc, String dep, String correo, String clave, String puesto)
        {
            this.nombre = nombre;
            this.rfc = rfc;
            this.departamento = dep;
            this.correo = correo;
            this.clave = clave;
            this.puesto = puesto;
        }

        public Docente(String rfc, String x)
        {
            this.rfc = rfc;
        }

        public Docente(String nombre)
        {
            this.nombre = nombre;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public String getRFC()
        {
            return this.rfc;
        }

        public void setRFC(String rfc)
        {
            this.rfc = rfc;
        }
        public String getDepartamento()
        {
            return this.departamento;
        }

        public String getCorreo()
        {
            return correo;
        }

        public String getPuesto()
        {
            return puesto;
        }

        public void setPuesto(String nuevo)
        {
            puesto = nuevo;
        }

        public String Nombre { get => nombre; set => nombre = value; }
        public String RFC { get => rfc; set => rfc = value; }
        public String Departamento { get => departamento; set => departamento = value; }
        public String Correo { get => correo; set => correo = value; }
        public String Clave { get => clave; set => clave = value; }
        public String Puesto { get => puesto; set => puesto = value; }

    }
}
