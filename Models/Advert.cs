using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models
{
    public class Advert
    {
        [Key]
        public int AdvertId { get; set; }
        public bool TypeAdvert { get; set; }
        public string Telephone { get; set; }
        public DateTime AddDate { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Pieces { get; set; }
        public string UserId { get; set; }
       // public List<Photo> Photos { get; set; }
        public int AddressId { get; set; }
        public bool Deactivate { get; set; }
        public string Tags { get; set; }
    }
}
