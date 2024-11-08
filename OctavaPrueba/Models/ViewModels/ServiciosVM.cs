namespace OctavaPrueba.Models.ViewModels
{
    public class ServiciosVM
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; } // Puedes establecer un valor predeterminado como 1
    }
}
