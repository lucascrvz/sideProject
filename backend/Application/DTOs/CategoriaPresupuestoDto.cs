namespace Application.DTOs
{
    public class CategoriaPresupuestoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal MontoAsignado { get; set; }
        public decimal MontoGastado { get; set; }
    }
}
