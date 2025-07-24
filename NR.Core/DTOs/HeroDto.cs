namespace NR.Core.DTOs
{
    public class HeroDto
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public required string Tagline { get; set; }
        public required string ButtonText { get; set; }
        public required string ButtonUrl { get; set; }
    }
}