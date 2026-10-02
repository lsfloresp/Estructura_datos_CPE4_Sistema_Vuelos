using System.Collections.Generic;

namespace Estructura_datos_CPE4_Sistema_Vuelos.Modelos
{
    public class Grafo
    {
        private Dictionary<string, List<Vuelo>> listaAdyacencia;

        public Grafo()
        {
            listaAdyacencia = new Dictionary<string, List<Vuelo>>();
        }
        public void AgregarCiudad(string ciudad)
        {
            if (!listaAdyacencia.ContainsKey(ciudad))
            {
                listaAdyacencia[ciudad] = new List<Vuelo>();
            }
        }
    }
}