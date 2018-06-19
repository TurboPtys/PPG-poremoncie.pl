using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public string Path { get; set; }
        public bool IsMain { get; set; }
        public int AdvertId { get; set; }
    }
}
