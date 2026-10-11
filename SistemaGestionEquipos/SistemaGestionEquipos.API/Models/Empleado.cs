
namespace SistemaGestionEquipos.API.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
