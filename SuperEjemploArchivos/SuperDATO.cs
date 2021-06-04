using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SuperEjemploArchivos
{
    [Serializable]//serializacion binaria
    [XmlInclude(typeof(ClaseHija))]// incluye tambien los atributos de la clase hija  
    public class SuperDATO
    {
        string texto;
        int numero;

        //si quiero que un atributo no se guarde, no le declaro la prop 
        public string Texto
        {
            get
            {
                return this.texto;
            }
            set
            {
                this.texto = value;
            }
        }

        public int Numero
        {
            get
            {
                return this.numero;
            }
            set
            {
                this.numero = value;
            }

        }


        public SuperDATO() //constructor sin parametros
        {

        }

        public SuperDATO(string texto,int numero)
        {
            this.texto = texto;
            this.numero = numero;

        }

        public override string ToString()
        {
            return $"texto: {this.texto} y numero: {this.numero}";
        }

    }
}
