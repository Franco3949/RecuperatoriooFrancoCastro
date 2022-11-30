using ParcialDeJohnDoe.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialDeJohnDoe.Datos
{
    public static class ManejadorDeArchivoSecuencial
    {
        private static string archivo = "Ortoedro.txt";
        public static void GuardarEnArchivo(List<Ortoedro> lista)
        {
            using (var escritor = new StreamWriter(archivo))
            {
                foreach (var ortoedro in lista)
                {
                    string linea = ConstruirLinea(ortoedro);
                    escritor.WriteLine(linea);
                }
            }
        }

        private static string ConstruirLinea(Ortoedro ortoedro)
        {
            return $"{ortoedro.AristaA}|{ortoedro.AristaB}|{ortoedro.AristaC}|{ortoedro.Relleno.GetHashCode()}";
        }

        public static List<Ortoedro> LeerArchivo()
        {
            List<Ortoedro> lista = new List<Ortoedro>();
            using (StreamReader lector = new StreamReader(archivo))
            {
                while (!lector.EndOfStream)//Mientas no sea el fin del flujo de datos, es decir, mientras pueda seguir leyendo el archivo
                {
                    string linea = lector.ReadLine();
                    Ortoedro ortoedro = CrearOrtoedro(linea);
                    lista.Add(ortoedro);
                }
            }
            return lista;
        }

        private static Ortoedro CrearOrtoedro(string linea)
        {
            var campos = linea.Split('|');
            Ortoedro ortoedro = new Ortoedro()
            {
                AristaA = int.Parse(campos[0]),
                AristaB = int.Parse(campos[1]),
                AristaC = int.Parse(campos[2]),
                Relleno = (Relleno)int.Parse(campos[3])
            };
            return ortoedro;
        }
    }
}
