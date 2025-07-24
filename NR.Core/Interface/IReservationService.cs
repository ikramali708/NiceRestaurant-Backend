namespace NR.Domain.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task AddAsync(ReservationDto dto);
        Task UpdateAsync(int id, ReservationDto dto);
        Task<bool> CheckAvailabilityAsync(DateTime date, TimeSpan time);
    }
}