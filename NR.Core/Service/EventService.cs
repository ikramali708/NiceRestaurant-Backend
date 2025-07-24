using NiceRestaurantBackend.Core.DTOs;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _repository;

        public EventService(IRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var @event = await _repository.GetByIdAsync(id);
            if (@event == null)
                throw new Exception($"Event with ID {id} not found.");
            return @event;
        }

        public async Task AddAsync(EventDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Description))
                throw new ArgumentException("Title and Description are required.");

            var @event = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                ImageUrl = dto.ImageUrl
            };
            await _repository.AddAsync(@event);
        }

        public async Task UpdateAsync(int id, EventDto dto)
        {
            var @event = await _repository.GetByIdAsync(id);
            if (@event == null)
                throw new Exception($"Event with ID {id} not found.");

            @event.Title = dto.Title;
            @event.Description = dto.Description;
            @event.Date = dto.Date;
            @event.ImageUrl = dto.ImageUrl;
            await _repository.UpdateAsync(@event);
        }

        public async Task DeleteAsync(int id)
        {
            var @event = await _repository.GetByIdAsync(id);
            if (@event == null)
                throw new Exception($"Event with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}