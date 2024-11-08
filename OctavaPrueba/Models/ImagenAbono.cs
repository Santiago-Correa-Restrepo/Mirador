using System;
using System.Collections.Generic;

namespace OctavaPrueba.Models;

public partial class ImagenAbono
{
    public int IdImagenAbono { get; set; }

    public int IdImagen { get; set; }

    public int IdAbono { get; set; }

    public virtual Abono IdAbonoNavigation { get; set; } = null!;

    public virtual Imagene IdImagenNavigation { get; set; } = null!;
}
