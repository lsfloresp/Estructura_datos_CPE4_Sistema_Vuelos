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
        public void MostrarMatrizAdyacencia()
        {
            List<string> ciudades = ObtenerCiudades();

            Console.Write("          ");

            foreach (string ciudad in ciudades)
            {
            Console.Write($"{ciudad,-12}");
            }

            Console.WriteLine();

            foreach (string origen in ciudades)
            {
            Console.Write($"{origen,-10}");

            foreach (string destino in ciudades)
            {
                double precio = 0;

                foreach (Vuelo vuelo in listaAdyacencia[origen])
                {
                    if (vuelo.Destino == destino)
                    {
                        precio = vuelo.Precio;
                        break;
                    }
                }

                Console.Write($"{precio,-12:F2}");
            }

            Console.WriteLine();
            }
        }
    }
}