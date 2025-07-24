namespace NR.Domain.Interfaces
{
    public interface IContactSubmissionService
    {
        Task<IEnumerable<ContactSubmission>> GetAllAsync();
        Task<ContactSubmission> GetByIdAsync(int id);
        Task AddAsync(ContactSubmissionDto dto);
    }
}