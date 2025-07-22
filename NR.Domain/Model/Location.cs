using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.Domain.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; } // e.g., "40.7128,-74.0060"
    }
}
