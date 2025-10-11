using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PresupuestoService
    {
        private readonly IPresupuestoRepository _repo;

        public PresupuestoService(IPresupuestoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PresupuestoMensualDto> CrearPresupuestoMensualAsync(int usuarioId, int año, int mes, decimal montoTotal)
        {
            var presupuesto = new PresupuestoMensual
            {
                UsuarioId = usuarioId,
                Year = año,
                Mes = mes,
                MontoTotal = montoTotal,
                MontoGastado = 0,
                Categorias = new List<CategoriaPresupuesto>()
            };

            await _repo.AddPresupuestoAsync(presupuesto);
            await _repo.SaveChangesAsync();

            return new PresupuestoMensualDto
            {
                Id = presupuesto.IdPresupuestoMensual,
                Year = presupuesto.Year,
                Mes = presupuesto.Mes,
                MontoTotal = presupuesto.MontoTotal,
                MontoGastado = presupuesto.MontoGastado,
                Categorias = new List<CategoriaPresupuestoDto>()
            };
        }

        public async Task<List<PresupuestoMensualDto>> ObtenerPresupuestosUsuarioAsync(int usuarioId)
        {
            var presupuestos = await _repo.GetPresupuestosPorUsuarioAsync(usuarioId);
            return presupuestos.Select(p => new PresupuestoMensualDto
            {
                Id = p.IdPresupuestoMensual,
                Year = p.Year,
                Mes = p.Mes,
                MontoTotal = p.MontoTotal,
                MontoGastado = p.MontoGastado,
                Categorias = p.Categorias?.Select(c => new CategoriaPresupuestoDto
                {
                    Id = c.IdCategoriaPresupuesto,
                    Nombre = c.Nombre,
                    MontoAsignado = c.MontoAsignado,
                    MontoGastado = c.MontoGastado
                }).ToList() ?? new List<CategoriaPresupuestoDto>()
            }).ToList();
        }

        public async Task<CategoriaPresupuestoDto> AgregarCategoriaAsync(int presupuestoId, string nombre, decimal montoAsignado)
        {
            var categoria = new CategoriaPresupuesto
            {
                PresupuestoMensualId = presupuestoId,
                Nombre = nombre,
                MontoAsignado = montoAsignado,
                MontoGastado = 0
            };

            var added = await _repo.AddCategoriaAsync(categoria);
            await _repo.SaveChangesAsync();

            return new CategoriaPresupuestoDto
            {
                Id = added.IdCategoriaPresupuesto,
                Nombre = added.Nombre,
                MontoAsignado = added.MontoAsignado,
                MontoGastado = added.MontoGastado
            };
        }

        public async Task<bool> ActualizarGastoCategoriaAsync(int categoriaId, decimal montoGastado)
        {
            var categoria = await _repo.GetCategoriaByIdAsync(categoriaId);
            if (categoria == null) return false;

            categoria.MontoGastado += montoGastado;

            // actualizar monto del presupuesto padre
            // cargamos presupuesto para actualizar MontoGastado acumulado
            var presupuesto = await _repo.GetPresupuestoConCategoriasAsync(categoria.PresupuestoMensualId);
            if (presupuesto != null)
            {
                presupuesto.MontoGastado += montoGastado;
            }

            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistePresupuestoAsync(int categoriaId, int año, int mes)
        {
            return await _repo.ExistePresupuestoAsync(categoriaId, año, mes);
        }

        public async Task SaveChangesAsync()
        {
            await _repo.SaveChangesAsync();
        }
    }
}
