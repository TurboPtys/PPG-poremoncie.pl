using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPG_projekt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PPG_projekt.Models.Dictionary;

namespace PPG_projekt.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Dict> Dictionaries { get; set; }
        public DbSet<DictionaryObject> DictionaryObjects { get; set; }
        public DbSet<DictionarySubObject> DictionarySubObjects { get; set; }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //        Customize the ASP.NET Identity model and override the defaults if needed.
    //        For example, you can rename the ASP.NET Identity table names and more.
    //        Add your customizations after calling base.OnModelCreating(builder);
    //}
}
}