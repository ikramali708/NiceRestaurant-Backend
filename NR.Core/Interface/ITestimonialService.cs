using NiceRestaurantBackend.Core.DTOs;
using NR.Domain.Model;

namespace NR.Domain.Interfaces
{
    public interface ITestimonialService
    {
        Task<IEnumerable<Testimonial>> GetAllAsync();
        Task<Testimonial> GetByIdAsync(int id);
        Task AddAsync(TestimonialDto dto);
        Task UpdateAsync(int id, TestimonialDto dto);
        Task DeleteAsync(int id);
    }
}