using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NR.Core.DTOs;
using NR.Core.Interface;

namespace NiceRestuarant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.AuthenticateAsync(dto.Username, dto.Password);
            if (token == null) return Unauthorized("Invalid credentials");
            return Ok(new TokenDto { Token = token });
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser()
        {
            var username = User.Identity.Name;
            var user = await IAuthService.GetUserAsync(username);
            return Ok(user);
        }
    }

}