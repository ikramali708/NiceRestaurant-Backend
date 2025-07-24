using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interfaces;

namespace NiceRestuarant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactSubmissionService _contactService;

        public ContactController(IContactSubmissionService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var submissions = await _contactService.GetAllAsync();
            return Ok(submissions);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var submission = await _contactService.GetByIdAsync(id);
                return Ok(submission);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactSubmissionDto dto)
        {
            try
            {
                await _contactService.AddAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = 0 }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}