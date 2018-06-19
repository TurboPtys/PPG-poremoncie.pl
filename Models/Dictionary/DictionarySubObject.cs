using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.Dictionary
{
    public class DictionarySubObject
    {
        [Key]
        public int DictionarySubObjectId { get; set; }
        public int DictionaryObjectId { get; set; }
        public string DictionarySubObjectName { get; set; }
        public bool Deactivate { get; set; }

    }
}
