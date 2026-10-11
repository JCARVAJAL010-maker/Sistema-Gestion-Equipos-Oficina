namespace SistemaGestionEquipos.API.DTOs
{
    public class EquipoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public int? EmpleadoId { get; set; }
    }
}