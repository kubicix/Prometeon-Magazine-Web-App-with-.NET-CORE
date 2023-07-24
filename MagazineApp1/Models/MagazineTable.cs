using System;
using System.Collections.Generic;

namespace MagazineApp1.Models
{
    public partial class MagazineTable
    {
        public int Magid { get; set; }
        public string? Magtitle { get; set; }
        public string? Magdesc { get; set; }
        public string? Magurl { get; set; }
        public DateTime? Magdate { get; set; }
        public string? Magimg { get; set; }
    }
}
