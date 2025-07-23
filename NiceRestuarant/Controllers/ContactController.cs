using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NR.Core.Interface;
using NR.Domain.Interface;
using NR.Domain.Model;
using NR.Core.DTOs;

namespace NiceRestuarant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<NR.Core.DTOs.ContactSubmission> _repository;
        private readonly IEmailService _emailService;

        public ContactController(IRepository<NR.Core.DTOs.ContactSubmission> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var submissions = await _repository.GetAllAsync();
            return Ok(submissions);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactSubmissionDto dto)
        {
            var submission = new NR.Core.DTOs.ContactSubmission
            {
                Name = dto.Name,
                Email = dto.Email,
                Subject = dto.Subject,
                Message = dto.Message,
                Timestamp = DateTime.UtcNow
            };
            await _repository.AddAsync(submission);

            await _emailService.SendEmailAsync("restaurant@example.com", dto.Subject, dto.Message);
            return Ok();
        }
    }
}