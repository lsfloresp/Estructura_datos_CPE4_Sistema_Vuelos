using System;
using System.Collections.Generic;

namespace Estructura_datos_CPE4_Sistema_Vuelos.Modelos
{
    public class Grafo
    {
        private Dictionary<string, List<Vuelo>> listaAdyacencia;
        private Dictionary<string, string> nombresAeropuertos;

        public Grafo()
        {
            listaAdyacencia = new Dictionary<string, List<Vuelo>>();
            nombresAeropuertos = new Dictionary<string, string>();
        }

        // ==============================
        // REGISTRO DE AEROPUERTOS
        // ==============================

        public void AgregarCiudad(string codigo)
        {
            if (!listaAdyacencia.ContainsKey(codigo))
            {
                listaAdyacencia[codigo] = new List<Vuelo>();
            }
        }

        public void AgregarAeropuerto(string codigo, string ciudad)
        {
            codigo = codigo.Trim().ToUpper();

            AgregarCiudad(codigo);

            if (!nombresAeropuertos.ContainsKey(codigo))
            {
                nombresAeropuertos.Add(codigo, ciudad);
            }
        }

        public void MostrarAeropuertosDisponibles()
        {
            Console.WriteLine("AEROPUERTOS DISPONIBLES");
            Console.WriteLine("--------------------------------");

            foreach (string codigo in listaAdyacencia.Keys)
            {
                Console.WriteLine($"{codigo} - {nombresAeropuertos[codigo]}");
            }
        }

        // ==============================
        // REGISTRO DE VUELOS
        // ==============================

        public void AgregarVuelo(string origen, string destino, double precio)
        {
            origen = origen.Trim().ToUpper();
            destino = destino.Trim().ToUpper();

            AgregarCiudad(origen);
            AgregarCiudad(destino);

            Vuelo vueloIda = new Vuelo(origen, destino, precio);
            Vuelo vueloRegreso = new Vuelo(destino, origen, precio);

            listaAdyacencia[origen].Add(vueloIda);
            listaAdyacencia[destino].Add(vueloRegreso);
        }

        // ==============================
        // LISTA DE ADYACENCIA
        // ==============================

        public void MostrarListaAdyacencia()
        {
            foreach (var aeropuerto in listaAdyacencia)
            {
                Console.WriteLine(
                    $"{aeropuerto.Key} - {nombresAeropuertos[aeropuerto.Key]}"
                );

                foreach (Vuelo vuelo in aeropuerto.Value)
                {
                    Console.WriteLine(
                        $"  -> {vuelo.Destino} | ${vuelo.Precio:F2}"
                    );
                }
            }
        }

        // ==============================
        // MATRIZ DE ADYACENCIA
        // ==============================

        public List<string> ObtenerAeropuertos()
        {
            return new List<string>(listaAdyacencia.Keys);
        }

        public void MostrarMatrizAdyacencia()
        {
            List<string> aeropuertos = ObtenerAeropuertos();

            Console.Write("          ");

            foreach (string aeropuerto in aeropuertos)
            {
                Console.Write($"{aeropuerto,-12}");
            }

            Console.WriteLine();

            foreach (string origen in aeropuertos)
            {
                Console.Write($"{origen,-10}");

                foreach (string destino in aeropuertos)
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

        // ==============================
        // BFS
        // ==============================

        public void BFS(string inicio)
        {
            string? inicioReal = BuscarAeropuerto(inicio);

            if (inicioReal == null)
            {
                Console.WriteLine("El aeropuerto no existe.");
                return;
            }

            HashSet<string> visitados = new HashSet<string>();
            Queue<string> cola = new Queue<string>();

            visitados.Add(inicioReal);
            cola.Enqueue(inicioReal);

            Console.Write("BFS: ");

            while (cola.Count > 0)
            {
                string aeropuertoActual = cola.Dequeue();

                Console.Write($"{aeropuertoActual} ");

                foreach (Vuelo vuelo in listaAdyacencia[aeropuertoActual])
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

        // ==============================
        // DFS
        // ==============================

        public void DFS(string inicio)
        {
            string? inicioReal = BuscarAeropuerto(inicio);

            if (inicioReal == null)
            {
                Console.WriteLine("El aeropuerto no existe.");
                return;
            }

            HashSet<string> visitados = new HashSet<string>();

            Console.Write("DFS: ");

            DFSRecursivo(inicioReal, visitados);

            Console.WriteLine();
        }

        private void DFSRecursivo(
            string aeropuerto,
            HashSet<string> visitados)
        {
            visitados.Add(aeropuerto);

            Console.Write($"{aeropuerto} ");

            foreach (Vuelo vuelo in listaAdyacencia[aeropuerto])
            {
                if (!visitados.Contains(vuelo.Destino))
                {
                    DFSRecursivo(vuelo.Destino, visitados);
                }
            }
        }

        // ==============================
        // BÚSQUEDA DE CAMINO
        // ==============================

        public bool ExisteCamino(string origen, string destino)
        {
            string? origenReal = BuscarAeropuerto(origen);
            string? destinoReal = BuscarAeropuerto(destino);

            if (origenReal == null || destinoReal == null)
            {
                return false;
            }

            HashSet<string> visitados = new HashSet<string>();
            Queue<string> cola = new Queue<string>();

            visitados.Add(origenReal);
            cola.Enqueue(origenReal);

            while (cola.Count > 0)
            {
                string aeropuertoActual = cola.Dequeue();

                if (aeropuertoActual == destinoReal)
                {
                    return true;
                }

                foreach (Vuelo vuelo in listaAdyacencia[aeropuertoActual])
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

        // ==============================
        // BÚSQUEDA DEL VUELO MÁS BARATO
        // ==============================

        public void BuscarRutaMasBarata(string origen, string destino)
        {
            string? origenReal = BuscarAeropuerto(origen);
            string? destinoReal = BuscarAeropuerto(destino);

            if (origenReal == null || destinoReal == null)
            {
                Console.WriteLine("Uno de los aeropuertos no existe.");
                return;
            }

            Dictionary<string, double> distancias =
                new Dictionary<string, double>();

            Dictionary<string, string?> anteriores =
                new Dictionary<string, string?>();

            foreach (string aeropuerto in listaAdyacencia.Keys)
            {
                distancias[aeropuerto] = double.MaxValue;
                anteriores[aeropuerto] = null;
            }

            distancias[origenReal] = 0;

            HashSet<string> visitados = new HashSet<string>();

            while (visitados.Count < listaAdyacencia.Count)
            {
                string aeropuertoActual = "";
                double menorDistancia = double.MaxValue;

                foreach (string aeropuerto in listaAdyacencia.Keys)
                {
                    if (!visitados.Contains(aeropuerto) &&
                        distancias[aeropuerto] < menorDistancia)
                    {
                        menorDistancia = distancias[aeropuerto];
                        aeropuertoActual = aeropuerto;
                    }
                }

                if (aeropuertoActual == "")
                {
                    break;
                }

                visitados.Add(aeropuertoActual);

                foreach (Vuelo vuelo in listaAdyacencia[aeropuertoActual])
                {
                    if (!visitados.Contains(vuelo.Destino))
                    {
                        double nuevaDistancia =
                            distancias[aeropuertoActual] + vuelo.Precio;

                        if (nuevaDistancia < distancias[vuelo.Destino])
                        {
                            distancias[vuelo.Destino] = nuevaDistancia;
                            anteriores[vuelo.Destino] = aeropuertoActual;
                        }
                    }
                }
            }

            if (distancias[destinoReal] == double.MaxValue)
            {
                Console.WriteLine(
                    "No existe una ruta entre los aeropuertos indicados."
                );

                return;
            }

            List<string> ruta = new List<string>();

            string? aeropuertoRuta = destinoReal;

            while (aeropuertoRuta != null)
            {
                ruta.Add(aeropuertoRuta);
                aeropuertoRuta = anteriores[aeropuertoRuta];
            }

            ruta.Reverse();

            Console.WriteLine();
            Console.WriteLine("RESULTADO DE LA BÚSQUEDA");
            Console.WriteLine("--------------------------------");

            Console.WriteLine(
                $"Origen: {origenReal} - {nombresAeropuertos[origenReal]}"
            );

            Console.WriteLine(
                $"Destino: {destinoReal} - {nombresAeropuertos[destinoReal]}"
            );

            Console.WriteLine();

            Console.Write("Ruta más económica: ");

            for (int i = 0; i < ruta.Count; i++)
            {
                string codigo = ruta[i];

                Console.Write($"{codigo}");

                if (i < ruta.Count - 1)
                {
                    Console.Write(" → ");
                }
            }

            Console.WriteLine();

            Console.WriteLine(
                $"Precio total: ${distancias[destinoReal]:F2}"
            );
        }

        // ==============================
        // BÚSQUEDA DE AEROPUERTO
        // ==============================

        private string? BuscarAeropuerto(string codigo)
        {
            string entrada = codigo.Trim();

            foreach (string codigoReal in listaAdyacencia.Keys)
            {
                if (string.Equals(
                    codigoReal,
                    entrada,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return codigoReal;
                }
            }

            return null;
        }
    }
}