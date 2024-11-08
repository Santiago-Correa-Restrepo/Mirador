using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OctavaPrueba.Models.ViewModels
{
    public class CrearReservaVM
    {
        public int IdReserva { get; set; }
        public string NroDocumentoCliente { get; set; } = null!;

        // Paquetes
        // Lista de paquetes para llenar el dropdown
        public List<PaqueteInfoVM> PaquetesDisponibles { get; set; }
        public List<PaqueteInfoVM> PaquetesSeleccionados { get; set; } = new List<PaqueteInfoVM>();
        public ClienteInfoVM ClienteInfo { get; set; } = new ClienteInfoVM();

        public List<ServiciosVM> ServiciosDisponibles { get; set; }
        public List<ServiciosVM> ServiciosSeleccionados { get; set; } = new List<ServiciosVM>();

        // Nuevas propiedades para la reserva
        public DateTime FechaReserva { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Today;
        public DateTime FechaFinalizacion { get; set; } = DateTime.Today;
        public int NroPersonas { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public double MontoTotal { get; set; }
        public int MetodoPago { get; set; }
        public decimal IVA { get; set; } = 0.19m; // 19%
        public double Iva { get; internal set; }

        [Display (Name = "Nombre")]
        public int IdEstadoReserva { get; set; }


        // Nuevas propiedades para cargar métodos de pago y estados de reserva
        public List<MetodoPagoVM> MetodosPago { get; set; } = new List<MetodoPagoVM>();
        public List<EstadoReservaVM> EstadosReserva { get; set; } = new List<EstadoReservaVM>();

        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string TipoDocumento { get; set; }

    }
}
