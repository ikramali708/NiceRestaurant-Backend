using NiceRestaurantBackend.Core.DTOs;
using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task AddAsync(EventDto dto);
        Task UpdateAsync(int id, EventDto dto);
        Task DeleteAsync(int id);
    }
}