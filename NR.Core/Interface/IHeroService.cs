using NR.Core.DTOs;
using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetAllAsync();
        Task<Hero> GetByIdAsync(int id);
        Task AddAsync(HeroDto dto);
        Task UpdateAsync(int id, HeroDto dto);
        Task DeleteAsync(int id);
    }
}