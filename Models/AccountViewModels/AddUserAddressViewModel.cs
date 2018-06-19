using Microsoft.AspNetCore.Mvc.Rendering;
using PPG_projekt.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.AccountViewModels
{
    public class AddUserAddressViewModel
    {

        public string User { get; set; }
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string Street { get; set; }
        public int HouseNr { get; set; }
        public int LocalNr { get; set; }
        public string PostCode { get; set; }
        public int Phone { get; set; }
        public IEnumerable<SelectListItem> Voivodeships { get; set; }
    }

}