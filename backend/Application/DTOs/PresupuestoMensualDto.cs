namespace Application.DTOs
{
    public class PresupuestoMensualDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Mes { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoGastado { get; set; }
        public List<CategoriaPresupuestoDto> Categorias { get; set; }
    }
}
