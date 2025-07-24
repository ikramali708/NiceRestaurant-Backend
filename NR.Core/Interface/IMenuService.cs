using NiceRestaurantBackend.Core.DTOs;
using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<Menu> GetByIdAsync(int id);
        Task AddAsync(MenuDto dto);
        Task UpdateAsync(int id, MenuDto dto);
        Task DeleteAsync(int id);
    }
}