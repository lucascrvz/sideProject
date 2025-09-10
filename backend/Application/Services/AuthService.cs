using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;

        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string HashPassword(User user, string plainPassword)
        {
            return _passwordHasher.HashPassword(user, plainPassword);
        }

        public PasswordVerificationResult VerifyPassword(
            User user,
            string plainPassword,
            string hashedPassword
        )
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, plainPassword);
        }

        public string GenerateJwtToken(string username)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                // otros claims si los tienes
            };

            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("Clave JWT no configurada.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
