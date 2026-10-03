using Estructura_datos_CPE4_Sistema_Vuelos.Modelos;

Grafo grafo = new Grafo();

grafo.AgregarAeropuerto("UIO", "Quito");
grafo.AgregarAeropuerto("GYE", "Guayaquil");
grafo.AgregarAeropuerto("CUE", "Cuenca");
grafo.AgregarAeropuerto("BOG", "Bogotá");

grafo.AgregarVuelo("UIO", "GYE", 45);
grafo.AgregarVuelo("UIO", "CUE", 55);
grafo.AgregarVuelo("UIO", "BOG", 120);
grafo.AgregarVuelo("GYE", "CUE", 50);
grafo.AgregarVuelo("GYE", "BOG", 135);

int opcion;

do
{
    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine("       SISTEMA DE VUELOS BARATOS");
    Console.WriteLine("==========================================");
    Console.WriteLine("1. Aeropuertos disponibles");
    Console.WriteLine("2. Buscar vuelo más barato");
    Console.WriteLine("3. Visualizar mapa de vuelos");
    Console.WriteLine("0. Salir");
    Console.WriteLine("==========================================");
    Console.Write("Seleccione una opción: ");

     if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        opcion = -1;
    }

    Console.WriteLine();

    switch (opcion)
    {

        case 1:
            grafo.MostrarAeropuertosDisponibles();
            break;
            
        case 2:
            Console.WriteLine("BUSCAR VUELO MÁS BARATO");
            Console.WriteLine("----------------------------------------");

            Console.Write("Ingrese aeropuerto de origen (ej. UIO): ");
            string origen = Console.ReadLine()!.Trim();

            Console.Write("Ingrese aeropuerto de destino (ej. GYE): ");
            string destino = Console.ReadLine()!.Trim();

            grafo.BuscarRutaMasBarata(origen, destino);
            break;

        case 3:
            string rutaImagen = Path.Combine(
                AppContext.BaseDirectory,
                "imagenes",
                "graph.png"
            );

            if (File.Exists(rutaImagen))
            {
                System.Diagnostics.Process.Start(
                    "explorer.exe",
                    $"\"{rutaImagen}\""
                );
            }
            else
            {
                Console.WriteLine("No se encontró graph.png.");
            }

            break;
    }

    if (opcion != 0)
    {
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

} while (opcion != 0);