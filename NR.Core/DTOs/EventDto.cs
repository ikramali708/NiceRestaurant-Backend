namespace NR.Core.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }
    }
}