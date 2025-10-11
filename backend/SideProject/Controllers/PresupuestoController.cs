using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SideProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly PresupuestoService _presupuestoService;

        public PresupuestoController(PresupuestoService presupuestoService)
        {
            _presupuestoService = presupuestoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPresupuesto(PresupuestoMensual presupuesto)
        {
            await _presupuestoService.CrearPresupuestoMensualAsync(presupuesto.UsuarioId, presupuesto.Year, presupuesto.Mes, presupuesto.MontoTotal);
            return Ok("Presupuesto creado correctamente");
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObtenerPorUsuario(int usuarioId)
        {
            var presupuestos = await _presupuestoService.ObtenerPresupuestosUsuarioAsync(usuarioId);
            return Ok(presupuestos);
        }
    }
}
