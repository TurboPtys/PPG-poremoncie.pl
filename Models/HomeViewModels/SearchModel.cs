using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.IndexViewModels
{
    public class SearchModel
    {

        public string Search { get; set; }
        public string Category { get; set; }
        public string Place { get; set; }
        public int Range { get; set; }
        public IEnumerable<SelectListItem> Categorys { get; set; }
    }


}
