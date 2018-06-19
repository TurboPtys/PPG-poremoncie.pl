using Microsoft.EntityFrameworkCore;
using PPG_projekt.Data;
using PPG_projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPG_projekt.Models.AdminViewModels;

namespace PPG_projekt.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AppUser>> GetIncompleteUserAsync()
        {
            var items = await _context.Users
            .Where(x=>x.Deactivate==false)
            .ToArrayAsync();
            return items;
        }

        public async Task<IEnumerable<AppUser>> GetIncompleteDeactivateUserAsync()
        {
            var items = await _context.Users
            .Where(x => x.Deactivate == true)
            .ToArrayAsync();
            return items;
        }

        public async Task<AppUser> FindUser(string id)
        {

            var user = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();

            return user;

        }

        public async Task<bool> IsUserDeactivate(string email)
        {
            var user = await _context.Users.Where(m => m.Email == email).FirstOrDefaultAsync();

            return user.Deactivate;
        }

        public async Task<IEnumerable<AppUser>> Search(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                var UserI = await _context.Users.Where(m => m.Id.Contains(searchString)).ToArrayAsync();
                var UserE = await _context.Users.Where(m => m.Email.Contains(searchString)).ToArrayAsync();

                UsersListViewModel all = new UsersListViewModel();

                if(UserI != null && UserE !=null)
                {
                   return all.Users = UserI.Concat(UserE);
                }
                else if(UserE == null)
                {
                    return UserI;
                }
                else if (UserI == null)
                {
                    return UserE;
                }
                
            }

            return await _context.Users.ToArrayAsync();
        }

        public async Task<bool> DeleteUserAsync(string ID)
        {
            var user = new AppUser { Id = ID };
            _context.Users.Attach(user);
            _context.Users.Remove(user);


            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool>DeactivateUser(string ID)
        {
            var user = await _context.Users.Where(m => m.Id == ID).FirstOrDefaultAsync();
            _context.Entry(user).Property(a => a.Deactivate).CurrentValue = true;
            _context.SaveChanges();

            return user.Deactivate;
        }

        public async Task<bool> ActivateUser(string ID)
        {
            var user = await _context.Users.Where(m => m.Id == ID).FirstOrDefaultAsync();
            _context.Entry(user).Property(a => a.Deactivate).CurrentValue = false;
            _context.SaveChanges();

            return !user.Deactivate;
        }
    }
}
