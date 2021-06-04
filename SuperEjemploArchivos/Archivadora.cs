using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace SuperEjemploArchivos
{
    /// <summary>
    /// Clase que se encarga tanto de archivar como de recuperar datos
    /// </summary>
    public static class Archivadora
    {
        /// <summary>
        /// Guarda el string recibido como parametro en el path especificado por parametro. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="datos"></param>
        /// <returns>Retorna true en caso de exito, false si no pudo guardar el archivo.</returns>
        public static bool GuardarTexto(string path,string datos)
        {
            bool pudeGuardar = true;
            StreamWriter escritor = new StreamWriter(path);

            using (escritor)//uso el using para ahorrarme el close
            {
                escritor.WriteLine(datos);

                if (!File.Exists(path)) //si no existe, no se guardo nada = rip, retorno false.
                    pudeGuardar = false;

            }
            return pudeGuardar;
        }

        /// <summary>
        /// Devuelve el texto leido del path pasado como parametro
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LeerTexto(string path)
        {
            string stringLeido = "";

            if(File.Exists(path))//rogamos a todos los santos que exista O usamos el File.Exists
            {
                StreamReader lector = new StreamReader(path);
                stringLeido += lector.ReadToEnd(); //otros metodos utilizables ReadLine, Read (lee de a un caracter a la vez)             
            }

            return stringLeido;
        }


        #region serializacion x m l 


        /*
         * ¿Qué es la serializacion? (patrocinado por la ppt de la catedra)
            Es el proceso de convertir un objeto en memoria en una secuencia lineal de bytes.
            ¿Para qué sirve?

                Para pasarlo a otro proceso.

                Para pasarlo a otra máquina.

                Para grabarlo en disco.

                Para grabarlo en una base de datos.
         * */

        /*  reglas t e c n i c a s
         *  
         *  Una clase debe tener un constructor por defecto para que XmlSerializer
            pueda serializarla.
            Sólo se pueden serializar los atributos y propiedades públicas.
            Los métodos no se pueden serializar.
         * 
         * 
         * */
        public static bool SerializarXml<T>(T objeto,string path)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(T)); // objeto que sabe serializar, le indico el tipo de objeto que se va a serializar
            XmlTextWriter escritorXml = new XmlTextWriter(path,Encoding.UTF8); //objeto que sabe escribir, especifico donde se guarda el xml y su codificacion
            bool pudeSerializar = true;
            using(escritorXml) //devuelta el using para ahorrarme el close
            {
                try
                {
                    serializador.Serialize(escritorXml, objeto);
                }
                catch
                {
                    //lanzar otra excepcion etc
                    pudeSerializar = false;
                }

               
            }
            return pudeSerializar;
        }

        /// <summary>
        /// Deserealiza el archivo que se encuentra en el path pasado por parametro y retorna un objeto con los datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserealizarXml<T>(string path)
        {
            if (!File.Exists(path)) //si el path no existe, no tiene sentido seguir
                return default;

            XmlTextReader lectorXml = new XmlTextReader(path); //path desde donde se va a leer el archivo
            XmlSerializer serializador = new XmlSerializer(typeof(T));
            T retorno = default(T); //devuelvo por defecto el valor por defecto de T

            using(lectorXml)
            {              
               retorno = (T)serializador.Deserialize(lectorXml); //debo castear el retorno de Deserealize                           
            }

            return retorno;
        }
        #endregion


        #region serializacion b i n a r i a
        /* 
         Para poder hacer una serialización binaria se debe agregar el marcador
         [Serializable]
        
         Serializa y Deserializa objetos en formato binario.
         Se encuentra en el espacio de nombres
         System.Runtime.Serialization.Formatters.Binary
         Puede serializar atributos públicos y privados.
         Una clase debe tener un constructor por defecto para que BinaryFormatter
         pueda serializarla.
         Los métodos más importantes de la clase BinaryFormatter son:

         Serialize y Deserialize
        */

        /// <summary>
        /// Serializa en formato  b i n a r i o
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <param name="path"></param>
        /// <returns>retorna true en caso de exito o false si no pudo serializar correctamente.</returns>
        public static bool SerializarBin<T>(T objeto, string path)
        {
            FileStream escritorBinario = new FileStream(path,FileMode.Create);
            BinaryFormatter serializadorBinario = new BinaryFormatter();
            bool pudeSerializar = true;

            using (escritorBinario) //me ahorro el close nuevaMentE
            {
                try
                {
                    serializadorBinario.Serialize(escritorBinario, objeto);
                }
                catch
                {
                    pudeSerializar = false;
                }               
            }

            return pudeSerializar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserealizarBin<T>(string path)
        {
            if (!File.Exists(path))
                return default;


            T objeto = default;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                objeto = (T)binaryFormatter.Deserialize(fileStream); //hay que c a s t e a r

            }
            catch
            {
                return default;
            }

            return objeto;
        }


        #endregion


    }
}
