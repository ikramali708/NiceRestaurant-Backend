namespace NR.Core.DTOs
{
    public class TestimonialDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Review { get; set; }
        public int Rating { get; set; }
    }
}