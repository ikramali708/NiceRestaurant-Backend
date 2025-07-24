using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface IChefService
    {
        Task<IEnumerable<Chef>> GetAllAsync();
        Task<Chef> GetByIdAsync(int id);
        Task AddAsync(ChefDto dto, IFormFile? file);
        Task UpdateAsync(int id, ChefDto dto, IFormFile? file);
        Task DeleteAsync(int id);
    }
}