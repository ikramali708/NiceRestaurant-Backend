using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NR.Core.DTOs;
using NR.Domain.Interface;
using NR.Domain.Model;

namespace NiceRestaurantBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IRepository<Hero> _repository;

        public HeroController(IRepository<Hero> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var heroes = await _repository.GetAllAsync();
            return Ok(heroes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] HeroDto dto)
        {
            var hero = new Hero
            {
                ImageUrl = dto.ImageUrl,
                Tagline = dto.Tagline,
                ButtonText = dto.ButtonText,
                ButtonUrl = dto.ButtonUrl
            };
            await _repository.AddAsync(hero);
            return CreatedAtAction(nameof(Get), new { id = hero.Id }, hero);
        }
    }
}