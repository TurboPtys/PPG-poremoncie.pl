using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Models.AdminViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }
    }
}
