using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Cuota> Cuotas { get; set; }
        public DbSet<PresupuestoMensual> PresupuestosMensuales { get; set; }
        public DbSet<CategoriaPresupuesto> CategoriasPresupuesto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === USUARIO ===
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.IdUsuario);
                entity.Property(u => u.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .IsRequired();
            });

            // === ORDEN ===
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(o => o.IdOrden);
                entity.Property(o => o.Descripcion)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(o => o.MontoTotal)
                      .HasPrecision(18, 2);

                entity.HasOne(o => o.Usuario)
                      .WithMany(u => u.Ordenes)
                      .HasForeignKey(o => o.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Cuotas)
                      .WithOne(c => c.Orden)
                      .HasForeignKey(c => c.OrdenId);
            });

            // === CUOTA ===
            modelBuilder.Entity<Cuota>(entity =>
            {
                entity.HasKey(c => c.IdCuota);
                entity.Property(c => c.Monto)
                      .HasPrecision(18, 2);

                entity.Property(c => c.FechaVencimiento)
                      .IsRequired();
            });

            modelBuilder.Entity<PresupuestoMensual>(entity =>
            {
                entity.HasKey(p => p.IdPresupuestoMensual);
                entity.HasIndex(p => new { p.UsuarioId, p.Year, p.Mes }).IsUnique();

                entity.HasMany(p => p.Categorias)
                    .WithOne(c => c.PresupuestoMensual)
                    .HasForeignKey(c => c.PresupuestoMensualId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // === CATEGORIA PRESUPUESTO ===
            modelBuilder.Entity<CategoriaPresupuesto>(entity =>
            {
                entity.HasKey(c => c.IdCategoriaPresupuesto);
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(c => c.MontoAsignado).HasPrecision(18, 2);
                entity.Property(c => c.MontoGastado).HasPrecision(18, 2);
            });
        }
    }
}
