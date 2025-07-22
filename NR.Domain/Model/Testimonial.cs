using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.Domain.Model
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}
