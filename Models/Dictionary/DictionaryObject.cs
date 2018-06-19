using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.Dictionary
{
    public class DictionaryObject
    {
        [Key]
        public int DictionaryObjectId { get; set; }
        public int DictionaryId { get; set; }
        public string DictionaryObjectName { get; set; }
        public bool Deactivate { get; set; }
        public bool SubObj { get; set; }
    }
}
