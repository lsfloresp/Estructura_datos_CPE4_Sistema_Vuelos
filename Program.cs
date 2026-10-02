using Estructura_datos_CPE4_Sistema_Vuelos.Modelos;

Grafo grafo = new Grafo();

grafo.AgregarVuelo("Quito", "Guayaquil", 45);
grafo.AgregarVuelo("Quito", "Cuenca", 55);
grafo.AgregarVuelo("Quito", "Bogotá", 120);

grafo.AgregarVuelo("Guayaquil", "Quito", 45);
grafo.AgregarVuelo("Guayaquil", "Cuenca", 50);
grafo.AgregarVuelo("Guayaquil", "Bogotá", 135);

grafo.AgregarVuelo("Cuenca", "Quito", 55);
grafo.AgregarVuelo("Cuenca", "Guayaquil", 50);

grafo.AgregarVuelo("Bogotá", "Quito", 120);
grafo.AgregarVuelo("Bogotá", "Guayaquil", 135);

grafo.MostrarListaAdyacencia();

Console.WriteLine();
Console.WriteLine("MATRIZ DE ADYACENCIA");
grafo.MostrarMatrizAdyacencia();

Console.WriteLine();
grafo.BFS("Quito");