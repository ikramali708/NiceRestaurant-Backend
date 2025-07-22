using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.Domain.Model
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string? Caption { get; set; }
    }
}
