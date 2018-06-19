using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.Dictionary
{
    public class Dict
    {
        [Key]
        public int DictionaryId { get; set; }
        public string DictionaryName { get; set; }
        public bool Deactivate { get; set; }
    }
}
