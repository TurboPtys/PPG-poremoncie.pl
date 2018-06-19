using PPG_projekt.Models;
using PPG_projekt.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> Search(string searchString);
        Task<IEnumerable<Address>> GetIncompleteAddressAsync(AppUser user);
        Task<IEnumerable<Address>> GetIncompleteAddressAsync();
        Task<IEnumerable<Address>> GetIncompleteDesactivateAddressAsync();
        Task<AddressDetalisViewModel> DetalisAddress(int id);
        Task<bool> DeleteAddressAsync(int ID);
        Task<bool> DeactivateAddressAsync(int ID);
        Task<bool> ActivateAddressAsync(int ID);
        Task<bool> DeactivateAllUserAddressAsync(string ID);
        Task<bool> DeleteAllAddressesOfUserAsync(string ID);
        Task<bool> AddAddressAsync(Address address, string uID);

    }
}
