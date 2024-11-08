using System;
using System.Collections.Generic;

namespace OctavaPrueba.Models;

public partial class DetalleReservaServicio
{
    public int IdDetalleReservaServicio { get; set; }

    public int IdServicio { get; set; }

    public int IdReserva { get; set; }

    public int Cantidad { get; set; }

    public double Precio { get; set; }

    public  Reserva IdReservaNavigation { get; set; } = null!;

    public  Servicio IdServicioNavigation { get; set; } = null!;

}
