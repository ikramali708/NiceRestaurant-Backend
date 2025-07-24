namespace NR.Core.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
    }
}