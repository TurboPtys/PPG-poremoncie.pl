using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PPG_projekt.Models
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }
        public string Nip { get; set; }
        public bool Deactivate { get; set; }
        /*
        [Key]
        public int UserID { get; set; }
        [Required()]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nazwa musi składa się z 2 znaków.")]
        //[Required(ErrorMessage = "Proszę podać Imię", AllowEmptyStrings = false)]
        public string Name { get; set; }
        // [Required(ErrorMessage = "Proszę podać hasło", AllowEmptyStrings = false)]
        [Required()]
        public string Password { get; set; }

        public string Nip { get; set; }

        // public List<User> Users { get; set; }
        */
    }

}