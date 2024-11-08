namespace OctavaPrueba.Models.ViewModels
{
    public class DetalleReservaVM
    {
        public int IdReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public string NroDocumentoCliente { get; set; }
        public string EstadoReserva { get; set; }
        public string MetodoPago { get; set; }
        public decimal MontoTotal { get; set; }

        // Listas para los paquetes y servicios
        public List<PaqueteInfoVM> Paquetes { get; set; } = new List<PaqueteInfoVM>();
        public List<ServiciosVM> Servicios { get; set; } = new List<ServiciosVM>();
    }
}
