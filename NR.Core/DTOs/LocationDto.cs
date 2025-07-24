namespace NR.Core.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public required string Coordinates { get; set; }
    }
}