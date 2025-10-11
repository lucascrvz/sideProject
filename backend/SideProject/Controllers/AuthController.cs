using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<Usuario> _passwordHasher;
    private readonly AuthService _authService;

    public AuthController(AppDbContext context, AuthService authService)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Usuario>();
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario UsuarioDto)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Nombre == UsuarioDto.Nombre))
            return BadRequest("El nombre de usuario ya está en uso.");

        var Usuario = new Usuario { Nombre = UsuarioDto.Nombre };

        Usuario.PasswordHash = _passwordHasher.HashPassword(Usuario, UsuarioDto.PasswordHash);

        var token = _authService.GenerateJwtToken(Usuario.Nombre);

        _context.Usuarios.Add(Usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetUsuario),
            new { id = Usuario.IdUsuario},
            new
            {
                Usuario.IdUsuario,
                Usuario.Nombre,
                Token = token,
            }
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Usuario loginDto)
    {
        var Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == loginDto.Nombre);

        if (Usuario == null)
            return Unauthorized("Usuario o contraseña incorrectos.");

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            Usuario,
            Usuario.PasswordHash,
            loginDto.PasswordHash
        );

        if (verificationResult == PasswordVerificationResult.Failed)
            return Unauthorized("Usuario o contraseña incorrectos.");

        return Ok(
            new
            {
                Usuario.IdUsuario,
                Usuario.Nombre,
                Token = _authService.GenerateJwtToken(Usuario.Nombre),
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var Usuario = await _context.Usuarios.FindAsync(id);
        if (Usuario == null)
            return NotFound();

        return Ok(new { Usuario.IdUsuario, Usuario.Nombre });
    }
}
