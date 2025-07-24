using NR.Core.DTOs;
using NR.Core.Interface;
using NR.Domain.Entities;
using NR.Domain.Interface;
using NR.Domain.Interfaces;

namespace NR.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IEmailService _emailService;

        public ReservationService(IRepository<Reservation> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var reservation = await _repository.GetByIdAsync(id);
            if (reservation == null)
                throw new Exception($"Reservation with ID {id} not found.");
            return reservation;
        }

        public async Task AddAsync(ReservationDto dto)
        {
            // Validate availability
            if (!await CheckAvailabilityAsync(dto.Date, dto.Time))
                throw new Exception("Time slot is already booked.");

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

            // Send confirmation email
            await _emailService.SendEmailAsync(dto.Email, "Reservation Confirmation",
                $"Your reservation for {dto.Date:yyyy-MM-dd} at {dto.Time} is confirmed.");
        }

        public async Task UpdateAsync(int id, ReservationDto dto)
        {
            var reservation = await _repository.GetByIdAsync(id);
            if (reservation == null)
                throw new Exception($"Reservation with ID {id} not found.");

            reservation.Status = dto.Status;
            await _repository.UpdateAsync(reservation);
        }

        public async Task<bool> CheckAvailabilityAsync(DateTime date, TimeSpan time)
        {
            var existing = (await _repository.GetAllAsync())
                .Any(r => r.Date.Date == date.Date && r.Time == time && r.Status != "Canceled");
            return !existing;
        }
    }
}