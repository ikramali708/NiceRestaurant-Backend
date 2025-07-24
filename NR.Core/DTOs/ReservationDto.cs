using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.Core.DTOs
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int PartySize { get; set; }
        public string? SpecialRequests { get; set; }
        public string Status { get; set; } // e.g., Pending, Confirmed, Canceled
    }
}
