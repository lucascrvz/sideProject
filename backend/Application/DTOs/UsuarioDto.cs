using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UsuarioDto
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Texto plano
    }

    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
