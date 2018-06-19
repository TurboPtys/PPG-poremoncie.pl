using Microsoft.EntityFrameworkCore;
using PPG_projekt.Data;
using PPG_projekt.Models;
using PPG_projekt.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Services
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;
        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetIncompleteAddressAsync()
        {
            var items = await _context.Addresses.Where(x=>x.Deactivate==false).ToArrayAsync();
            return items;
        }

        public async Task<IEnumerable<Address>> GetIncompleteDesactivateAddressAsync()
        {
            var items = await _context.Addresses.Where(x => x.Deactivate == true).ToArrayAsync();
            return items;
        }

        public async Task<AddressDetalisViewModel> DetalisAddress(int id)
        {

            var ads = await _context.Addresses.Where(m => m.AdressId == id).FirstOrDefaultAsync();
            var us = await _context.Users.Where(m => m.Id == ads.User).FirstOrDefaultAsync();

            AddressDetalisViewModel item = new AddressDetalisViewModel { Address = ads, User = us };


            return item;
        }

        public async Task<bool> DeleteAddressAsync(int ID)
        {
            var address = new Address { AdressId = ID };
            _context.Addresses.Attach(address);
            _context.Addresses.Remove(address);


            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteAllAddressesOfUserAsync(string ID)
        {

            var items = await _context.Addresses
            .Where(x => x.User.Equals(ID))
            .ToArrayAsync();
            
            foreach(var i in items)
            {
                _context.Addresses.Attach(i);
                _context.Addresses.Remove(i);
               
            }

            var result =await _context.SaveChangesAsync();

            return result==1;
        }


        public async Task<bool> DeactivateAddressAsync(int ID)
        {

            var adr = await _context.Addresses.Where(m => m.AdressId == ID).FirstOrDefaultAsync();
            _context.Entry(adr).Property(a => a.Deactivate).CurrentValue = true;
            _context.SaveChanges();

            return adr.Deactivate;
        }

        public async Task<bool> ActivateAddressAsync(int ID)
        {

            var adr = await _context.Addresses.Where(m => m.AdressId == ID).FirstOrDefaultAsync();
            _context.Entry(adr).Property(a => a.Deactivate).CurrentValue = false;
            _context.SaveChanges();

            return !adr.Deactivate;
        }

        public async Task<bool> DeactivateAllUserAddressAsync(string ID)
        {

            var items = await _context.Addresses
           .Where(x => x.User.Equals(ID))
           .ToArrayAsync();

            foreach (var i in items)
            {
                _context.Entry(i).Property(a => a.Deactivate).CurrentValue = true;

            }

            var result = await _context.SaveChangesAsync();

            return result == 1;


        }


        public async Task<IEnumerable<Address>> GetIncompleteAddressAsync(AppUser user)
        {
            var items = await _context.Addresses
           .Where(x => x.User.Equals(user.Id))
            .ToArrayAsync();
            return items;
        }

        public async Task<bool> AddAddressAsync(Address newAddress, string uID)
        {
            var entity = new Address
            {
                City = newAddress.City,
                HouseNr = newAddress.HouseNr,
                LocalNr = newAddress.LocalNr,
                Phone = newAddress.Phone,
                Street = newAddress.Street,
                PostCode = newAddress.PostCode,
                Voivodeship = newAddress.Voivodeship,
                User = uID


            };

            _context.Addresses.Add(entity);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }


        public async Task<IEnumerable<Address>> Search(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                int x = 0;
                if (Int32.TryParse(searchString, out x))
                {

                    x = Int32.Parse(searchString);
                }
                var AdvertI = await _context.Addresses.Where(m => m.AdressId == x).ToArrayAsync();
                return AdvertI;

            }

            return await _context.Addresses.ToArrayAsync();

        }
    }
}
