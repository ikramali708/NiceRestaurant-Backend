using NiceRestaurantBackend.Core.DTOs;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IRepository<Testimonial> _repository;

        public TestimonialService(IRepository<Testimonial> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Testimonial>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Testimonial> GetByIdAsync(int id)
        {
            var testimonial = await _repository.GetByIdAsync(id);
            if (testimonial == null)
                throw new Exception($"Testimonial with ID {id} not found.");
            return testimonial;
        }

        public async Task AddAsync(TestimonialDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Review))
                throw new ArgumentException("Name and Review are required.");
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            var testimonial = new Testimonial
            {
                Name = dto.Name,
                Review = dto.Review,
                Rating = dto.Rating
            };
            await _repository.AddAsync(testimonial);
        }

        public async Task UpdateAsync(int id, TestimonialDto dto)
        {
            var testimonial = await _repository.GetByIdAsync(id);
            if (testimonial == null)
                throw new Exception($"Testimonial with ID {id} not found.");

            testimonial.Name = dto.Name;
            testimonial.Review = dto.Review;
            testimonial.Rating = dto.Rating;
            await _repository.UpdateAsync(testimonial);
        }

        public async Task DeleteAsync(int id)
        {
            var testimonial = await _repository.GetByIdAsync(id);
            if (testimonial == null)
                throw new Exception($"Testimonial with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}