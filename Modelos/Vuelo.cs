namespace Estructura_datos_CPE4_Sistema_Vuelos.Modelos
{
    public class Vuelo
    {
        public string Origen { get; set; }
        public string Destino { get; set; }
        public double Precio { get; set; }

        public Vuelo(string origen, string destino, double precio)
        {
            Origen = origen;
            Destino = destino;
            Precio = precio;
        }
    }
}