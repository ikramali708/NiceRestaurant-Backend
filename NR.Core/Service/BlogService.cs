using NiceRestaurantBackend.Core.DTOs;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;
using NR.Domain.Model;

namespace NR.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _repository;

        public BlogService(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception($"Blog with ID {id} not found.");
            return blog;
        }

        public async Task AddAsync(BlogDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Content))
                throw new ArgumentException("Title and Content are required.");

            var blog = new Blog
            {
                Title = dto.Title,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
                PublishDate = DateTime.UtcNow
            };
            await _repository.AddAsync(blog);
        }

        public async Task UpdateAsync(int id, BlogDto dto)
        {
            var blog = await _repository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception($"Blog with ID {id} not found.");

            blog.Title = dto.Title;
            blog.Content = dto.Content;
            blog.ImageUrl = dto.ImageUrl;
            await _repository.UpdateAsync(blog);
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception($"Blog with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}