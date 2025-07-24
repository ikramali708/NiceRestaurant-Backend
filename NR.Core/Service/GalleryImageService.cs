using Microsoft.AspNetCore.Http;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly IRepository<GalleryImage> _repository;
        private readonly string _imagePath;

        public GalleryImageService(IRepository<GalleryImage> repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _imagePath = Path.Combine(env.WebRootPath, "images");
            Directory.CreateDirectory(_imagePath); // Ensure directory exists
        }

        public async Task<IEnumerable<GalleryImage>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<GalleryImage> GetByIdAsync(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                throw new Exception($"GalleryImage with ID {id} not found.");
            return image;
        }

        public async Task AddAsync(GalleryImageDto dto, IFormFile? file)
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

            var galleryImage = new GalleryImage
            {
                ImageUrl = imageUrl,
                Caption = dto.Caption
            };
            await _repository.AddAsync(galleryImage);
        }

        public async Task UpdateAsync(int id, GalleryImageDto dto, IFormFile? file)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                throw new Exception($"GalleryImage with ID {id} not found.");

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

            image.ImageUrl = imageUrl;
            image.Caption = dto.Caption;
            await _repository.UpdateAsync(image);
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                throw new Exception($"GalleryImage with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}