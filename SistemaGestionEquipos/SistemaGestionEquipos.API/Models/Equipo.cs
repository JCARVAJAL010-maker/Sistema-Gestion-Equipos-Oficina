namespace SistemaGestionEquipos.API.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        // Relación con Empleado
        public int? EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
    }
}