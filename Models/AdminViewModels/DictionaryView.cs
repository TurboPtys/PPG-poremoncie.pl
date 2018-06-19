using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPG_projekt.Models.Dictionary;

namespace PPG_projekt.Models.AdminViewModels
{
    public class DictionaryView
    {
        
        public IEnumerable<Dict> Dictionaries { get; set; }
        public IEnumerable<DictionaryObject> DictionaryObjects { get;set; }
        public int Page { get; set; }
    }
}
