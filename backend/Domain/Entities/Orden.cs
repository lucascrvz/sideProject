namespace Domain.Entities
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public string? Descripcion { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime Fecha { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? CategoriaPresupuestoId { get; set; }
        public CategoriaPresupuesto? CategoriaPresupuesto { get; set; }

        public ICollection<Cuota>? Cuotas { get; set; }
    }
}
