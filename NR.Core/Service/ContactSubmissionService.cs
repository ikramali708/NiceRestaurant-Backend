using NR.Core.DTOs;
using NR.Core.Interface;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;

namespace NR.Core.Services
{
    public class ContactSubmissionService : IContactSubmissionService
    {
        private readonly IRepository<ContactSubmission> _repository;
        private readonly IEmailService _emailService;

        public ContactSubmissionService(IRepository<ContactSubmission> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<ContactSubmission>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContactSubmission> GetByIdAsync(int id)
        {
            var submission = await _repository.GetByIdAsync(id);
            if (submission == null)
                throw new Exception($"ContactSubmission with ID {id} not found.");
            return submission;
        }

        public async Task AddAsync(ContactSubmissionDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) ||
                string.IsNullOrEmpty(dto.Subject) || string.IsNullOrEmpty(dto.Message))
                throw new ArgumentException("Name, Email, Subject, and Message are required.");

            var submission = new ContactSubmission
            {
                Name = dto.Name,
                Email = dto.Email,
                Subject = dto.Subject,
                Message = dto.Message,
                Timestamp = DateTime.UtcNow
            };
            await _repository.AddAsync(submission);

            await _emailService.SendEmailAsync(dto.Email, "Thank You for Your Submission",
                $"Dear {dto.Name},<br/>Thank you for contacting NiceRestaurant. We will respond to your inquiry soon.");
        }
    }
}