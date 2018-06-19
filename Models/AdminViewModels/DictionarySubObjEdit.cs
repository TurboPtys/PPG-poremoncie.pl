using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.AdminViewModels
{
    public class DictionarySubObjEdit
    {
        public IEnumerable<SelectListItem> DictionaryObjects { get; set; }
        public string DictionaryObjID { get; set; }
        public string Name { get; set; }
        public int DictionarySubObjID { get; set; }

    }
}
