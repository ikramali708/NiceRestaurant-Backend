using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.Domain.Model
{
    public class Hero
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Tagline { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
    }
}
