using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NR.Core.Interface;
using NR.Domain.Interface;
using NR.Domain.Model;

namespace NiceRestuarant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IEmailService _emailService;

        public ReservationController(IRepository<Reservation> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var reservations = await _repository.GetAllAsync();
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservationDto dto)
        {
            var existing = (await _repository.GetAllAsync())
                .Any(r => r.Date == dto.Date && r.Time == dto.Time && r.Status != "Canceled");
            if (existing)
            {
                return BadRequest("Time slot is already booked.");
            }

            var reservation = new Reservation
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Date = dto.Date,
                Time = dto.Time,
                PartySize = dto.PartySize,
                SpecialRequests = dto.SpecialRequests,
                Status = "Pending"
            };
            await _repository.AddAsync(reservation);

            await _emailService.SendEmailAsync(dto.Email, "Reservation Confirmation",
                $"Your reservation for {dto.Date} at {dto.Time} is confirmed.");
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ReservationDto dto)
        {
            var reservation = await _repository.GetByIdAsync(id);
            if (reservation == null) return NotFound();

            reservation.Status = dto.Status; // e.g., Confirm or Cancel
            await _repository.UpdateAsync(reservation);
            return Ok(reservation);
        }
    }
}