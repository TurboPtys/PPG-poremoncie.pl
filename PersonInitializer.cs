using Microsoft.AspNetCore.Identity;
using PPG_projekt.Data;
using PPG_projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PPG_projekt
{
    public class PersonInitializer
    {

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public static class Constants
        {
            public const string AdministratorRole = "Admin";
        }

        public PersonInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task Seed()
        {


            string[] voi = new string[] { "Dolnośląskie", "Kujawsko-pomorskie", "Lubelskie", "Lubuskie", "Łódzkie", "Małopolskie", "Mazowieckie", "Opolskie", "Podkarpackie", "Podlaskie", "Pomorskie", "Śląskie", "Świętokrzyskie", "Warmińsko-mazurskie", "Wielkopolskie", "Zachodniopomorskie" };
            string[] dict = new string[] { "Voivodeships", "Categories" };
            string[] cat = new string[] { "Brak","Farby", "Gwoździe" };
            string[] user = new string[] { "qq@wp.pl", "pp@wp.pl","kk@o2.pl","jj@gmail.com" };


            for (int i = 0; i < dict.Length; i++)
            {
                var catgory = _context.Dictionaries.Where(x => x.DictionaryName.Equals(dict[i])).FirstOrDefault();

                if (catgory == null)
                {
                    catgory = new Models.Dictionary.Dict
                    {
                        Deactivate = false,
                        DictionaryName = dict[i]
                    };

                    _context.Dictionaries.Add(catgory);
                    _context.SaveChanges();
                }
            }

            for (int i = 0; i < voi.Length; i++)
            {

                var voiv = _context.DictionaryObjects.Where(x => x.DictionaryObjectName.Equals(voi[i])).FirstOrDefault();

                if (voiv == null)
                {
                    voiv = new Models.Dictionary.DictionaryObject
                    {
                        Deactivate = false,
                        DictionaryId = 1,
                        DictionaryObjectName = voi[i],
                        SubObj = false

                    };

                    _context.DictionaryObjects.Add(voiv);
                    _context.SaveChanges();
                }
            }

            for (int i = 0; i < cat.Length; i++)
            {

                var cate = _context.DictionaryObjects.Where(x => x.DictionaryObjectName.Equals(cat[i])).FirstOrDefault();

                if (cate == null)
                {
                    cate = new Models.Dictionary.DictionaryObject
                    {
                        Deactivate = false,
                        DictionaryId = 2,
                        DictionaryObjectName = cat[i],
                        SubObj = true

                    };

                    _context.DictionaryObjects.Add(cate);
                    _context.SaveChanges();
                }
            }

            for (int i = 0; i < user.Length; i++)
            {
                var testUser = await _userManager.Users
          .Where(x => x.UserName.Equals(user[i])).FirstOrDefaultAsync();
                if (testUser == null)
                {
                    testUser = new AppUser
                    {
                        UserName = user[i],
                        Email = user[i]
                    };

                    await _userManager.CreateAsync(testUser, "Qwerty1!");
                 
                }

            }

            var address = _context.Addresses.First();

            if (address == null)
            {
                var us = _context.Users.Where(m => m.Email.Equals("qq@wp.pl")).First();
                address = new Address {
                    City = "Wroclaw",Deactivate=false,HouseNr=2,LocalNr=3,Phone=654741258,PostCode="52100",Street="Kleczkowska",User=us.Id,Voivodeship="Dolnośląskie"
                    
                };

                _context.Addresses.Add(address);
                _context.SaveChanges();

                us= _context.Users.Where(m => m.Email.Equals("pp@wp.pl")).First();
                address = new Address
                {
                    City = "Poznań",
                    Deactivate = false,
                    HouseNr = 2,
                    LocalNr = 3,
                    Phone = 514203887,
                    PostCode = "52100",
                    Street = "Kleczkowska",
                    User = us.Id,
                    Voivodeship = "Wielkopolska"

                };

            }

            var alreadyExists = await _roleManager.RoleExistsAsync(Constants.AdministratorRole);
            if (!alreadyExists) 
            await _roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));

            string adminName = "admin@PPG.local";

            var testAdmin = await _userManager.Users
           .Where(x => x.UserName.Equals(adminName)).FirstOrDefaultAsync();
            if (testAdmin == null)
            {
                testAdmin = new AppUser
                {
                    UserName = adminName,
                    Email = adminName
                };

                await _userManager.CreateAsync(testAdmin, "Admin@1");
                await _userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
            }



        }

    }
}
