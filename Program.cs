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

int opcion;

do
{
    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine("       SISTEMA DE VUELOS");
    Console.WriteLine("==========================================");
    Console.WriteLine("1. Mostrar lista de adyacencia");
    Console.WriteLine("2. Mostrar matriz de adyacencia");
    Console.WriteLine("3. Recorrido BFS");
    Console.WriteLine("4. Recorrido DFS");
    Console.WriteLine("5. Buscar caminos entre ciudades");
    Console.WriteLine("6. Buscar vuelo más barato");
    Console.WriteLine("7. Buscar ruta más barata");
    Console.WriteLine("0. Salir");
    Console.WriteLine("==========================================");
    Console.Write("Seleccione una opción: ");

    opcion = int.Parse(Console.ReadLine()!);

    Console.WriteLine();

    switch (opcion)
    {
        case 1:
            Console.WriteLine("LISTA DE ADYACENCIA");
            grafo.MostrarListaAdyacencia();
            break;

        case 2:
            Console.WriteLine("MATRIZ DE ADYACENCIA");
            grafo.MostrarMatrizAdyacencia();
            break;

        case 3:
            Console.WriteLine("RECORRIDO BFS");
            grafo.BFS("Quito");
            break;

        case 4:
            Console.WriteLine("RECORRIDO DFS");
            grafo.DFS("Quito");
            break;

        case 5:
            Console.WriteLine("BÚSQUEDA DE CAMINO");
            Console.WriteLine(grafo.ExisteCamino("Quito", "Bogotá"));
            break;

        case 6:
            Console.WriteLine("VUELO MÁS BARATO");
            Vuelo? vueloBarato = grafo.BuscarVueloMasBarato("Quito", "Guayaquil");

            if (vueloBarato != null)
            {
                Console.WriteLine($"Vuelo: {vueloBarato.Origen} -> {vueloBarato.Destino}");
                Console.WriteLine($"Precio: ${vueloBarato.Precio:F2}");
            }
            else
            {
                Console.WriteLine("No existe un vuelo directo entre esas ciudades.");
            }

            break;

        case 7:
            Console.WriteLine("RUTA MÁS BARATA");
            grafo.BuscarRutaMasBarata("Quito", "Cuenca");
            break;

        case 0:
            Console.WriteLine("Saliendo del sistema...");
            break;

        default:
            Console.WriteLine("Opción no válida.");
            break;
    }

    if (opcion != 0)
    {
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

} while (opcion != 0);