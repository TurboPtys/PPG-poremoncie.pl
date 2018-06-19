using PPG_projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Services
{
    public interface IUserService
    {
        Task<AppUser> FindUser(string id);
        Task<bool> IsUserDeactivate(string email);
        Task<IEnumerable<AppUser>> Search(string searchString);
        Task<bool> DeleteUserAsync(string ID);
        Task<bool> DeactivateUser(string ID);
        Task<bool> ActivateUser(string ID);
        Task<IEnumerable<AppUser>> GetIncompleteUserAsync();
        Task<IEnumerable<AppUser>> GetIncompleteDeactivateUserAsync();

    }
}
