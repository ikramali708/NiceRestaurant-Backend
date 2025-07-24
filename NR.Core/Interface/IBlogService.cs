using NiceRestaurantBackend.Core.DTOs;
using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task AddAsync(BlogDto dto);
        Task UpdateAsync(int id, BlogDto dto);
        Task DeleteAsync(int id);
    }
}