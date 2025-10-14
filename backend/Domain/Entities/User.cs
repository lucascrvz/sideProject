using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public ICollection<Orden>? Ordenes { get; set; }
    }
}
