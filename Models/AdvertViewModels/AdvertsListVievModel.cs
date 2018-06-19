using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.AdvertViewModels
{
    public class AdvertsListVievModel
    {
        public Address Address { get; set; }
        public AppUser User { get; set; }
        public Advert Advert { get; set; }
        public Photo Photo { get; set; }    
    }
}
