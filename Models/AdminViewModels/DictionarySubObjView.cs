using PPG_projekt.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.AdminViewModels
{
    public class DictionarySubObjView
    {

        
        public IEnumerable<DictionaryObject> DictionaryObjects { get; set; }
        public IEnumerable<DictionarySubObject> DictionarySubObj { get; set; }
        public int Page { get; set; }
    }
}
