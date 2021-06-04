using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperEjemploArchivos
{
    public class ClaseHija:SuperDATO
    {
        string masTexto;

        public string MasTexto
        {
            get
            {
                return this.masTexto;
            }
            set
            {
                this.masTexto = value;
            }
        }

        public ClaseHija()
        { }


        public ClaseHija(string texto,int numero,string masTexto):base(texto,numero)
        {
            this.masTexto = masTexto;
        }




    }
}
