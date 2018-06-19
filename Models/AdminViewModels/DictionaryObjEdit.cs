using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.AdminViewModels
{
    public class DictionaryObjEdit
    {
        public IEnumerable<SelectListItem> Dictionaries { get; set; }
        public string DictionaryID { get; set; }
        public string Name { get; set; }
        public bool SubObj { get; set; }
        public int DictionaryObjID { get; set; }


    }
}
