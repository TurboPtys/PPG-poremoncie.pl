using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models
{
    public class Address
    {
       

       // [Key]
       // int AddressId { get; set; }
       [Key]
        public int AdressId { get; set; }
            
        public string User { get; set; }
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string Street { get; set; }
        public int HouseNr { get; set; }
        public int LocalNr { get; set; }
        public string PostCode { get; set; }
        public int Phone { get; set; }
        public bool Deactivate { get; set; }
    }
}
