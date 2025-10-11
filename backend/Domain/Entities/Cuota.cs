using Domain.Enums;

namespace Domain.Entities
{
    public class Cuota
    {
        public int IdCuota { get; set; }
        public int NumeroCuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoCuota Estado { get; set; } = EstadoCuota.Pendiente;

        // Relaciones
        public int OrdenId { get; set; }
        public Orden? Orden { get; set; }
    }
}
