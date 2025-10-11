namespace Domain.Entities
{
    public class CategoriaPresupuesto
    {
        public int IdCategoriaPresupuesto { get; set; }
        public int PresupuestoMensualId { get; set; }
        public PresupuestoMensual? PresupuestoMensual { get; set; }

        public string? Nombre { get; set; }
        public decimal MontoAsignado { get; set; }
        public decimal MontoGastado { get; set; }

        public ICollection<Orden>? Ordenes { get; set; }
    }
}
