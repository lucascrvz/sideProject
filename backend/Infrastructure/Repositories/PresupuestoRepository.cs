using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly AppDbContext _context;

        public PresupuestoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPresupuestoAsync(PresupuestoMensual presupuesto)
        {
            await _context.PresupuestosMensuales.AddAsync(presupuesto);
        }

        public async Task<PresupuestoMensual?> GetPresupuestoConCategoriasAsync(int presupuestoId)
        {
            return await _context.PresupuestosMensuales
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.IdPresupuestoMensual == presupuestoId);
        }

        public async Task<List<PresupuestoMensual>> GetPresupuestosPorUsuarioAsync(int usuarioId)
        {
            return await _context.PresupuestosMensuales
                .Include(p => p.Categorias)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<CategoriaPresupuesto?> GetCategoriaByIdAsync(int categoriaId)
        {
            return await _context.CategoriasPresupuesto.FindAsync(categoriaId);
        }

        public async Task<CategoriaPresupuesto> AddCategoriaAsync(CategoriaPresupuesto categoria)
        {
            var result = await _context.CategoriasPresupuesto.AddAsync(categoria);
            return result.Entity;
        }

        public async Task<bool> ExistePresupuestoAsync(int usuarioId, int año, int mes)
        {
            return await _context.PresupuestosMensuales
                .AnyAsync(p => p.UsuarioId == usuarioId && p.Year == año && p.Mes == mes);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}
