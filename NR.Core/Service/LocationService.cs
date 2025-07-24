using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _repository;

        public LocationService(IRepository<Location> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null)
                throw new Exception($"Location with ID {id} not found.");
            return location;
        }

        public async Task AddAsync(LocationDto dto)
        {
            if (string.IsNullOrEmpty(dto.Address) || string.IsNullOrEmpty(dto.Coordinates))
                throw new ArgumentException("Address and Coordinates are required.");

            var location = new Location
            {
                Address = dto.Address,
                Coordinates = dto.Coordinates
            };
            await _repository.AddAsync(location);
        }

        public async Task UpdateAsync(int id, LocationDto dto)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null)
                throw new Exception($"Location with ID {id} not found.");

            location.Address = dto.Address;
            location.Coordinates = dto.Coordinates;
            await _repository.UpdateAsync(location);
        }

        public async Task DeleteAsync(int id)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null)
                throw new Exception($"Location with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}