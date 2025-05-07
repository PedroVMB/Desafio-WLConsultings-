using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WLConsultings.Application.Interfaces;
using WLConsultings.Domain.Core.Interfaces.Repositorys;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
            _passwordHasher = new PasswordHasher<ApplicationUser>();  // Instância do PasswordHasher
        }

        public async Task RegisterAsync(string name, string email, string password)
        {
            // Usando o PasswordHasher para gerar o hash da senha
            var user = new ApplicationUser
            {
                Name = name,
                Email = email
            };

            // Fazendo o hash da senha com o PasswordHasher
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            await _userRepo.AddAsync(user);
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            // Usando o PasswordHasher para verificar se a senha fornecida é válida
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials");

            // Se a senha estiver correta, cria os claims para o JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"])); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
