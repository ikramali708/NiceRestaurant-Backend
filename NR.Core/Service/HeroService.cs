using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class HeroService : IHeroService
    {
        private readonly IRepository<Hero> _repository;

        public HeroService(IRepository<Hero> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Hero> GetByIdAsync(int id)
        {
            var hero = await _repository.GetByIdAsync(id);
            if (hero == null)
                throw new Exception($"Hero with ID {id} not found.");
            return hero;
        }

        public async Task AddAsync(HeroDto dto)
        {
            if (string.IsNullOrEmpty(dto.ImageUrl) || string.IsNullOrEmpty(dto.Tagline) ||
                string.IsNullOrEmpty(dto.ButtonText) || string.IsNullOrEmpty(dto.ButtonUrl))
                throw new ArgumentException("ImageUrl, Tagline, ButtonText, and ButtonUrl are required.");

            var hero = new Hero
            {
                ImageUrl = dto.ImageUrl,
                Tagline = dto.Tagline,
                ButtonText = dto.ButtonText,
                ButtonUrl = dto.ButtonUrl
            };
            await _repository.AddAsync(hero);
        }

        public async Task UpdateAsync(int id, HeroDto dto)
        {
            var hero = await _repository.GetByIdAsync(id);
            if (hero == null)
                throw new Exception($"Hero with ID {id} not found.");

            hero.ImageUrl = dto.ImageUrl;
            hero.Tagline = dto.Tagline;
            hero.ButtonText = dto.ButtonText;
            hero.ButtonUrl = dto.ButtonUrl;
            await _repository.UpdateAsync(hero);
        }

        public async Task DeleteAsync(int id)
        {
            var hero = await _repository.GetByIdAsync(id);
            if (hero == null)
                throw new Exception($"Hero with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}