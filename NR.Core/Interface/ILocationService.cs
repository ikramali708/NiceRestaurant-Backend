using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
        Task AddAsync(LocationDto dto);
        Task UpdateAsync(int id, LocationDto dto);
        Task DeleteAsync(int id);
    }
}