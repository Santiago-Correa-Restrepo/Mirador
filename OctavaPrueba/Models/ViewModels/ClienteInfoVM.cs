namespace OctavaPrueba.Models.ViewModels
{
    public class ClienteInfoVM
    {
        public string NroDocumento { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string TipoDocumento { get; set; }
    }
}
