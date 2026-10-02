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
        public void AgregarVuelo(string origen, string destino, double precio)
        {
            AgregarCiudad(origen);
            AgregarCiudad(destino);

            Vuelo vuelo = new Vuelo(origen, destino, precio);

            listaAdyacencia[origen].Add(vuelo);
        }
        public void MostrarListaAdyacencia()
        {
            foreach (var ciudad in listaAdyacencia)
            {
                Console.WriteLine($"Ciudad: {ciudad.Key}");

                foreach (var vuelo in ciudad.Value)
                {
                    Console.WriteLine($"  -> {vuelo.Destino} | ${vuelo.Precio:F2}");
                }
            }
        }
        public List<string> ObtenerCiudades()
        {
            return new List<string>(listaAdyacencia.Keys);
        }
    }
}