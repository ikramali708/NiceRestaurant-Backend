using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface IGalleryImageService
    {
        Task<IEnumerable<GalleryImage>> GetAllAsync();
        Task<GalleryImage> GetByIdAsync(int id);
        Task AddAsync(GalleryImageDto dto, IFormFile? file);
        Task UpdateAsync(int id, GalleryImageDto dto, IFormFile? file);
        Task DeleteAsync(int id);
    }
}