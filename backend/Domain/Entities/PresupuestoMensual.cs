using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PresupuestoMensual
    {
        public int IdPresupuestoMensual { get; set; }
        public int UsuarioId { get; set; }

        [Required]
        public Usuario? Usuario { get; set; }

        public int Year { get; set; }
        public int Mes { get; set; }

        public decimal MontoTotal { get; set; }  // Dinero total disponible del mes
        public decimal MontoGastado { get; set; } // Se puede ir calculando

        public ICollection<CategoriaPresupuesto>? Categorias { get; set; }
    }
}
