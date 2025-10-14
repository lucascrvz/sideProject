using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<(bool Success, string Token, string Message)> RegisterAsync(UsuarioDto dto)
        {
            if (await _userRepository.ExistsByNameAsync(dto.Nombre))
                return (false, null, "El nombre ya existe");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email
            };

            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, dto.Password);

            await _userRepository.AddAsync(usuario);

            var token = GenerateJwtToken(usuario);
            return (true, token, null);
        }

        public async Task<(bool Success, string Token)> LoginAsync(LoginDto dto)
        {
            var usuario = await _userRepository.GetByNameAsync(dto.Nombre);
            if (usuario == null)
                return (false, null);

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return (false, null);

            var token = GenerateJwtToken(usuario);
            return (true, token);
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
