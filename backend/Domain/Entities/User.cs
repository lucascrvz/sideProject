using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
        public ICollection<Orden>? Ordenes { get; set; }
    }
}
