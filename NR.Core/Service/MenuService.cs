using Microsoft.Extensions.Configuration;
using NiceRestaurantBackend.Core.DTOs;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _repository;

        public MenuService(IRepository<Menu> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            var menu = await _repository.GetByIdAsync(id);
            if (menu == null)
                throw new Exception($"Menu with ID {id} not found.");
            return menu;
        }

        public async Task AddAsync(MenuDto dto)
        {
            // Basic validation
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Category))
                throw new ArgumentException("Name and Category are required.");

            var menu = new Menu
            {
                Category = dto.Category,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl
            };
            await _repository.AddAsync(menu);
        }

        public async Task UpdateAsync(int id, MenuDto dto)
        {
            var menu = await _repository.GetByIdAsync(id);
            if (menu == null)
                throw new Exception($"Menu with ID {id} not found.");

            menu.Category = dto.Category;
            menu.Name = dto.Name;
            menu.Description = dto.Description;
            menu.Price = dto.Price;
            menu.ImageUrl = dto.ImageUrl;
            await _repository.UpdateAsync(menu);
        }

        public async Task DeleteAsync(int id)
        {
            var menu = await _repository.GetByIdAsync(id);
            if (menu == null)
                throw new Exception($"Menu with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}