using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NR.Core.Interface;
using NR.Domain.Interface;
using NR.Domain.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NR.Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Admin> _adminRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<Admin> adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var admin = await GetUserAsync(username);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim(ClaimTypes.Role, admin.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Admin> GetUserAsync(string username)
        {
            return (await _adminRepository.GetAllAsync()).FirstOrDefault(a => a.Username == username);
        }
    }
}
