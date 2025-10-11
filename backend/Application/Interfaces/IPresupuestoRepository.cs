using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPresupuestoRepository
    {
        Task AddPresupuestoAsync(PresupuestoMensual presupuesto);
        Task<PresupuestoMensual?> GetPresupuestoConCategoriasAsync(int presupuestoId);
        Task<List<PresupuestoMensual>> GetPresupuestosPorUsuarioAsync(int usuarioId);
        Task<CategoriaPresupuesto?> GetCategoriaByIdAsync(int categoriaId);
        Task<CategoriaPresupuesto> AddCategoriaAsync(CategoriaPresupuesto categoria);
        Task<bool> ExistePresupuestoAsync(int usuarioId, int año, int mes);
        Task SaveChangesAsync();
    }
}
