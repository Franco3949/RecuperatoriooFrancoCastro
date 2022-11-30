using ParcialDeJohnDoe.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialDeJohnDoe.Datos
{
    public class RepositorioDeOrtoedros
    {
        private List<Ortoedro> listaOrtoedros;
        bool hayCambios = false;

        public RepositorioDeOrtoedros()
        {
            listaOrtoedros = new List<Ortoedro>();
            listaOrtoedros = ManejadorDeArchivoSecuencial.LeerArchivo();
        }

        public void Agregar(Ortoedro ortoedro)
        {
            listaOrtoedros.Add(ortoedro);
            hayCambios = true;
        }
        public void Borrar(Ortoedro ortoedro)
        {
            listaOrtoedros.Remove(ortoedro);
            hayCambios = true;
        }
        public void Editar(Ortoedro ortoedro)
        {
            hayCambios = true;
        }
        public List<Ortoedro> GetLista()
        {
            return listaOrtoedros;
        }
        public int GetCantidad()
        {
            return listaOrtoedros.Count;
        }

        public void GuardarEnArchivoSecuencial()
        {
            if (hayCambios)
            {
                ManejadorDeArchivoSecuencial.GuardarEnArchivo(listaOrtoedros);
            }
        }

        public List<Ortoedro> GetOrdenAscendente()
        {
            return listaOrtoedros.OrderBy(e=> e.Volumen()).ToList();
        }

        public List<Ortoedro> GetOrdenDescendente()
        {
            return listaOrtoedros.OrderByDescending(e=>e.Volumen()).ToList();
        }

        public List<Ortoedro> FiltrarPorColor(Relleno colorFiltrar)
        {
            return listaOrtoedros.Where(e => e.Relleno == colorFiltrar).ToList();
        }
    }
}
