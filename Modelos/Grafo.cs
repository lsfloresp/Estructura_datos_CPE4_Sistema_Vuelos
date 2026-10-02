using System;
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
        public void BFS(string inicio)
        {
            if (!listaAdyacencia.ContainsKey(inicio))
            {
                Console.WriteLine("La ciudad no existe en el grafo.");
                return;
            }

            HashSet<string> visitados = new HashSet<string>();
            Queue<string> cola = new Queue<string>();

            visitados.Add(inicio);
            cola.Enqueue(inicio);

            Console.Write("BFS: ");

            while (cola.Count > 0)
            {
                string ciudadActual = cola.Dequeue();

                Console.Write($"{ciudadActual} ");

                foreach (Vuelo vuelo in listaAdyacencia[ciudadActual])
                {
                    if (!visitados.Contains(vuelo.Destino))
                    {
                        visitados.Add(vuelo.Destino);
                        cola.Enqueue(vuelo.Destino);
                    }
            }
        }

        Console.WriteLine();
        }
        public void DFS(string inicio)
        {
            if (!listaAdyacencia.ContainsKey(inicio))
            {
                Console.WriteLine("La ciudad no existe en el grafo.");
                return;
            }

            HashSet<string> visitados = new HashSet<string>();

            Console.Write("DFS: ");

            DFSRecursivo(inicio, visitados);

            Console.WriteLine();
        }
        private void DFSRecursivo(string ciudad, HashSet<string> visitados)
        {
            visitados.Add(ciudad);

            Console.Write($"{ciudad} ");

            foreach (Vuelo vuelo in listaAdyacencia[ciudad])
            {
                if (!visitados.Contains(vuelo.Destino))
                {
                    DFSRecursivo(vuelo.Destino, visitados);
                }
            }
        }
        public bool ExisteCamino(string origen, string destino)
        {
            if (!listaAdyacencia.ContainsKey(origen) ||
                !listaAdyacencia.ContainsKey(destino))
            {
                return false;
            }

            HashSet<string> visitados = new HashSet<string>();
            Queue<string> cola = new Queue<string>();

            visitados.Add(origen);
            cola.Enqueue(origen);

            while (cola.Count > 0)
            {
                string ciudadActual = cola.Dequeue();

                if (ciudadActual == destino)
                {
                    return true;
                }

                foreach (Vuelo vuelo in listaAdyacencia[ciudadActual])
                {
                    if (!visitados.Contains(vuelo.Destino))
                    {
                        visitados.Add(vuelo.Destino);
                        cola.Enqueue(vuelo.Destino);
                    }
                }
            }

            return false;
        }
        public Vuelo? BuscarVueloMasBarato(string origen, string destino)
        {
            if (!listaAdyacencia.ContainsKey(origen))
            {
                return null;
            }

            Vuelo? vueloMasBarato = null;

            foreach (Vuelo vuelo in listaAdyacencia[origen])
            {
                if (vuelo.Destino == destino)
                {
                    if (vueloMasBarato == null || vuelo.Precio < vueloMasBarato.Precio)
                    {
                        vueloMasBarato = vuelo;
                    }
                }
            }

            return vueloMasBarato;
        }
        
        public void BuscarRutaMasBarata(string origen, string destino)
        {
            if (!listaAdyacencia.ContainsKey(origen) ||
                !listaAdyacencia.ContainsKey(destino))
            {
                Console.WriteLine("Una de las ciudades no existe en el grafo.");
                return;
            }

            Dictionary<string, double> distancias = new Dictionary<string, double>();
            Dictionary<string, string?> anteriores = new Dictionary<string, string?>();

            foreach (string ciudad in listaAdyacencia.Keys)
            {
                distancias[ciudad] = double.MaxValue;
                anteriores[ciudad] = null;
            }

            distancias[origen] = 0;

            HashSet<string> visitados = new HashSet<string>();

            while (visitados.Count < listaAdyacencia.Count)
            {
                string ciudadActual = "";
                double menorDistancia = double.MaxValue;

                foreach (string ciudad in listaAdyacencia.Keys)
                {
                    if (!visitados.Contains(ciudad) &&
                        distancias[ciudad] < menorDistancia)
                    {
                        menorDistancia = distancias[ciudad];
                        ciudadActual = ciudad;
                    }
                }

                if (ciudadActual == "")
                {
                    break;
                }

                visitados.Add(ciudadActual);

                foreach (Vuelo vuelo in listaAdyacencia[ciudadActual])
                {
                    if (!visitados.Contains(vuelo.Destino))
                    {
                        double nuevaDistancia =
                            distancias[ciudadActual] + vuelo.Precio;

                        if (nuevaDistancia < distancias[vuelo.Destino])
                        {
                            distancias[vuelo.Destino] = nuevaDistancia;
                            anteriores[vuelo.Destino] = ciudadActual;
                        }
                    }
                }
            }

            if (distancias[destino] == double.MaxValue)
            {
                Console.WriteLine("No existe una ruta entre las ciudades indicadas.");
                return;
            }

            List<string> ruta = new List<string>();
            string? ciudadRuta = destino;

            while (ciudadRuta != null)
            {
                ruta.Add(ciudadRuta);
                ciudadRuta = anteriores[ciudadRuta];
            }

            ruta.Reverse();

            Console.WriteLine($"Ruta más barata: {string.Join(" -> ", ruta)}");
            Console.WriteLine($"Costo total: ${distancias[destino]:F2}");
        }
    }
}