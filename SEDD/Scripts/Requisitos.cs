using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDD.Scripts
{
    public class Requisitos
    {
        String code;
        String desc;
        String date;
        String obligatorio;

        public Requisitos(String code, String desc, String date, String obligatorio)
        {
            this.code = code;
            this.desc = desc;
            this.date = date;
            this.obligatorio = obligatorio;
        }

        public String Code { get => this.code; set => this.code = value; }
        public String Descipcion { get => this.desc; set => this.desc = value; }
        public String Fecha { get => this.date; set => this.date = value; }
        public String Obligatorio { get => this.obligatorio; set => this.obligatorio = value; }

    }
}
