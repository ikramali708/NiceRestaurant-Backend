using Microsoft.AspNetCore.Http;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class ChefService : IChefService
    {
        private readonly IRepository<Chef> _repository;
        private readonly string _imagePath;

        public ChefService(IRepository<Chef> repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _imagePath = Path.Combine(env.WebRootPath, "images");
            Directory.CreateDirectory(_imagePath);
        }

        public async Task<IEnumerable<Chef>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Chef> GetByIdAsync(int id)
        {
            var chef = await _repository.GetByIdAsync(id);
            if (chef == null)
                throw new Exception($"Chef with ID {id} not found.");
            return chef;
        }

        public async Task AddAsync(ChefDto dto, IFormFile? file)
        {
            string imageUrl = dto.ImageUrl;
            if (file != null && file.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(_imagePath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                imageUrl = $"/images/{fileName}";
            }

            if (string.IsNullOrEmpty(imageUrl))
                throw new ArgumentException("ImageUrl or file is required.");

            var chef = new Chef
            {
                Name = dto.Name,
                Bio = dto.Bio,
                ImageUrl = imageUrl
            };
            await _repository.AddAsync(chef);
        }

        public async Task UpdateAsync(int id, ChefDto dto, IFormFile? file)
        {
            var chef = await _repository.GetByIdAsync(id);
            if (chef == null)
                throw new Exception($"Chef with ID {id} not found.");

            string imageUrl = dto.ImageUrl;
            if (file != null && file.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(_imagePath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                imageUrl = $"/images/{fileName}";
            }

            chef.Name = dto.Name;
            chef.Bio = dto.Bio;
            chef.ImageUrl = imageUrl;
            await _repository.UpdateAsync(chef);
        }

        public async Task DeleteAsync(int id)
        {
            var chef = await _repository.GetByIdAsync(id);
            if (chef == null)
                throw new Exception($"Chef with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}