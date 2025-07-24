namespace NR.Core.DTOs
{
    public class ChefDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Bio { get; set; }
        public required string ImageUrl { get; set; }
    }
}