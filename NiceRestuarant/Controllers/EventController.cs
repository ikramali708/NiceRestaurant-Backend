using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiceRestaurantBackend.Core.DTOs;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interfaces;

namespace NiceRestuarant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var @event = await _eventService.GetByIdAsync(id);
                return Ok(@event);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] EventDto dto)
        {
            try
            {
                await _eventService.AddAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = 0 }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] EventDto dto)
        {
            try
            {
                await _eventService.UpdateAsync(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _eventService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}