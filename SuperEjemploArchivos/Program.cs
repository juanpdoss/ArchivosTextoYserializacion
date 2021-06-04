using System;
using System.Collections.Generic;

namespace SuperEjemploArchivos
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //retorna el path al escritorio
            List<SuperDATO> lista = new List<SuperDATO>();

            string pathLista = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            pathLista += "/listaXml";


            string pathXml = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            pathXml += "/datoXml";


            string pathBin = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            pathBin += "/datoBin";

            string pathXmlHija = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            pathXmlHija += "/claseHijaxml";


            SuperDATO dato = new SuperDATO("Test guardar algo",34);

            path += "/texto.txt";

            if(!Archivadora.GuardarTexto(path,dato.ToString()))
            {
                Console.WriteLine("No guarde nada je");
            }

            Console.WriteLine("la siguiente linea contiene lo leido desde el archivo ubicado en: " + path);
            Console.ReadKey();
            Console.WriteLine(Archivadora.LeerTexto(path));


            Archivadora.SerializarXml<SuperDATO>(dato,pathXml); // le puedo pasar cualquier tipo de dato que cumpla los rekisitos para ser serializado

           


            Console.WriteLine("A continuacion, se deserealiza un objeto a partir de un xml, si esta todo ok deberian verse los datos por pantalla");
            Console.ReadLine();
            SuperDATO auxiliar = Archivadora.DeserealizarXml<SuperDATO>(pathXml);
            Console.WriteLine(auxiliar.ToString());


            Console.WriteLine("A continuacion, se serializara el objeto en formato binario");
            Console.ReadLine();

           if(!Archivadora.SerializarBin<SuperDATO>(dato,pathBin) )
           {
                Console.WriteLine("no pude serializar u_u"); 

           }

            Console.WriteLine("Se va a deserealizar partiendo del binario, cuidado que explota");
            SuperDATO auxiliarDos = Archivadora.DeserealizarBin<SuperDATO>(pathBin);
            Console.ReadKey();

            Console.WriteLine(auxiliarDos.ToString());


            ClaseHija hijita = new ClaseHija("serializame", 45, "todo xdios lo pido");



            lista.Add(dato);
            lista.Add(hijita);

            Archivadora.SerializarXml<ClaseHija>(hijita, pathXmlHija);



            Archivadora.SerializarXml<List<SuperDATO>>(lista,pathLista);



        }
    }
}
