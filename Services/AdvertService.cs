using PPG_projekt.Data;
using PPG_projekt.Models;
using PPG_projekt.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PPG_projekt.Models.IndexViewModels;
using PPG_projekt.Models.AccountViewModels;
using PPG_projekt.Models.AdvertViewModels;

namespace PPG_projekt.Services
{
    public class AdvertService : IAdvertService
    {

        private readonly ApplicationDbContext _context;

        public AdvertService(ApplicationDbContext context)
        {
            _context = context;
        }
    

       

        public async Task<IEnumerable<Advert>> Search(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                int x = 0;
                if(Int32.TryParse(searchString,out x))
                {
                    
                    x = Int32.Parse(searchString);
                }
                var AdvertI = await _context.Adverts.Where(m => m.AdvertId == x).ToArrayAsync();
                var AdvertU = await _context.Adverts.Where(m => m.UserId.Contains(searchString)).ToArrayAsync();
                var AdvertD = await _context.Adverts.Where(m => m.Description.Contains(searchString)).ToArrayAsync();

                AdvertsViewModel all = new AdvertsViewModel();

                if (AdvertI != null && AdvertU != null && AdvertD !=null)
                {
                    return all.Adverts = AdvertD.Concat(AdvertI.Concat(AdvertU));
                }
                else if (AdvertU == null)
                {
                    return all.Adverts=AdvertD.Concat(AdvertI);
                }
                else if (AdvertI == null)
                {
                    return all.Adverts = AdvertD.Concat(AdvertU);
                }

            }

            return await _context.Adverts.ToArrayAsync();

        }

        public async Task<IEnumerable<Advert>> GetIncompleteAdvertAsync()
        {
            var items = await _context.Adverts.Where(x=>x.Deactivate==false)
           .ToArrayAsync();
            return items;
        }

        public async Task<IEnumerable<Advert>> GetIncompleteDeactivateAdvertAsync()
        {
            var items = await _context.Adverts.Where(x => x.Deactivate == true)
           .ToArrayAsync();
            return items;
        }

        public async Task<IEnumerable<Advert>> GetIncompleteAdvertAsync(string ID)
        {
            var items = await _context.Adverts
            .Where(x => x.UserId.Equals(ID))
            .ToArrayAsync();
            return items;
        }

        public async Task<AdvertDetailViewModel> DetalisAdvert(int id)
        {
            var adv = await _context.Adverts.Where(m => m.AdvertId == id).FirstOrDefaultAsync();
            var ads = await _context.Addresses.Where(m => m.AdressId == adv.AddressId).FirstOrDefaultAsync();
            var us = await _context.Users.Where(m => m.Id ==adv.UserId).FirstOrDefaultAsync();

            AdvertDetailViewModel item = new AdvertDetailViewModel { Advert = adv, Address = ads,User=us };


            return item;
        }

        public async Task<IEnumerable<AdvertsListVievModel>> Search(SearchModel s)
        {
            //treść
            if(!String.IsNullOrEmpty(s.Search)){

                //treść,kategoria
                if (!s.Category.Equals("Brak"))
                {
                    //treść,katgoria,miejsce
                    if (!String.IsNullOrEmpty(s.Place))
                    {
                        var address = await _context.Addresses.Where(m => m.City.Contains(s.Place)).ToArrayAsync();
                        AdvertsListViewModel adv = new AdvertsListViewModel();
                        var al = new List<AdvertsListVievModel>();
                        foreach (var h in address)
                        {

                            var p = await _context.Adverts
                                .Where(m => m.Deactivate == false)
                                .Where(m=> m.Description.Contains(s.Search))
                                .Where(m => m.Category.Equals(s.Category))
                                .Where(m => m.AddressId == h.AdressId)
                                .ToArrayAsync();


                            foreach (var j in p)
                            {
                                var user = _context.Users.Where(m => m.Id == j.UserId).FirstOrDefault();
                                var img = await _context.Photos.Where(m => m.AdvertId == j.AdvertId && m.IsMain == true).FirstOrDefaultAsync();

                                al.Add(new AdvertsListVievModel { Address = h, Advert = j, User = user, Photo = img });
                            }

                        }
                        return al;

                    }
                    else//treść,kategoria,0
                    {
                        var adv = await _context.Adverts
                        .Where(m => m.Deactivate == false)
                        .Where(m=>m.Description.Contains(s.Search))
                        .Where(m => m.Category.Equals(s.Category)).ToArrayAsync();
                        var al = new List<AdvertsListVievModel>();

                        foreach (var i in adv)
                        {

                            var user = _context.Users.Where(m => m.Id == i.UserId).FirstOrDefault();
                            var img = _context.Photos.Where(m => m.AdvertId == i.AdvertId && m.IsMain == true).FirstOrDefault();
                            var add = _context.Addresses.Where(m => m.AdressId == i.AddressId).FirstOrDefault();
                            al.Add(new AdvertsListVievModel { Address = add, Advert = i, Photo = img, User = user });
                        }

                        return al;

                    }

                }
                else//treść,0
                {
                    //treść,0,miejsce
                    if (!String.IsNullOrEmpty(s.Place))
                    {
                        var address = await _context.Addresses.Where(m => m.City.Contains(s.Place)).ToArrayAsync();
                        AdvertsListViewModel adv = new AdvertsListViewModel();
                        var al = new List<AdvertsListVievModel>();
                        foreach (var h in address)
                        {

                            var p = await _context.Adverts
                                .Where(m => m.Deactivate == false)
                                .Where(m => m.AddressId == h.AdressId)
                                .Where(m=>m.Description.Contains(s.Search))
                                .ToArrayAsync();


                            foreach (var j in p)
                            {
                                var user = _context.Users.Where(m => m.Id == j.UserId).FirstOrDefault();
                                var img = await _context.Photos.Where(m => m.AdvertId == j.AdvertId && m.IsMain == true).FirstOrDefaultAsync();

                                al.Add(new AdvertsListVievModel { Address = h, Advert = j, User = user, Photo = img });
                            }

                        }
                        return al;

                    }
                    else //treść,0,0
                    {

                        var adv = await _context.Adverts.Where(m=>m.Description.Contains(s.Search)).ToArrayAsync();
                        var al = new List<AdvertsListVievModel>();
                        foreach (var i in adv)
                        {
                            var ads = await _context.Addresses.Where(x => x.AdressId == i.AddressId).FirstOrDefaultAsync();
                            var us = await _context.Users.Where(x => x.Id == i.UserId).FirstOrDefaultAsync();
                            var img = await _context.Photos.Where(x => x.AdvertId == i.AdvertId && x.IsMain == true)
                                .FirstOrDefaultAsync();
                            al.Add(new AdvertsListVievModel { Address = ads, Advert = i, User = us, Photo = img });
                        }

                        return al;

                    }
                }

            }
            else//0,
            {
                //0,kategoria
                if (!s.Category.Equals("Brak"))
                {
                    //0,katgoria,miejsce
                    if (!String.IsNullOrEmpty(s.Place))
                    {
                        var address = await _context.Addresses.Where(m => m.City.Contains(s.Place)).ToArrayAsync();
                        AdvertsListViewModel adv = new AdvertsListViewModel();
                        var al = new List<AdvertsListVievModel>();
                        foreach (var h in address)
                        {

                            var p = await _context.Adverts
                                .Where(m => m.Deactivate == false)
                                .Where(m => m.Category.Equals(s.Category))
                                .Where(m => m.AddressId == h.AdressId)
                                .ToArrayAsync();


                            foreach (var j in p)
                            {
                                var user = _context.Users.Where(m => m.Id == j.UserId).FirstOrDefault();
                                var img = await _context.Photos.Where(m => m.AdvertId == j.AdvertId && m.IsMain == true).FirstOrDefaultAsync();

                                al.Add(new AdvertsListVievModel { Address = h, Advert = j, User = user, Photo = img });
                            }

                        }
                        return al;

                    }
                    else//0,kategoria,0
                    {
                        var adv = await _context.Adverts
                        .Where(m => m.Deactivate == false)
                        .Where(m => m.Category.Equals(s.Category)).ToArrayAsync();
                        var al = new List<AdvertsListVievModel>();

                        foreach (var i in adv)
                        {

                            var user = _context.Users.Where(m => m.Id == i.UserId).FirstOrDefault();
                            var img = _context.Photos.Where(m => m.AdvertId == i.AdvertId && m.IsMain == true).FirstOrDefault();
                            var add = _context.Addresses.Where(m => m.AdressId == i.AddressId).FirstOrDefault();
                            al.Add(new AdvertsListVievModel { Address = add, Advert = i, Photo = img, User = user });
                        }

                        return al;

                    }

                }
                else//0,0
                {
                    //0,0,miejsce
                    if (!String.IsNullOrEmpty(s.Place))
                    {
                        var address = await _context.Addresses.Where(m => m.City.Contains(s.Place)).ToArrayAsync();
                        AdvertsListViewModel adv = new AdvertsListViewModel();
                        var al = new List<AdvertsListVievModel>();
                        foreach (var h in address)
                        {

                            var p = await _context.Adverts
                                .Where(m => m.Deactivate == false)
                                .Where(m => m.AddressId == h.AdressId)
                                .ToArrayAsync();


                            foreach (var j in p)
                            {
                                var user = _context.Users.Where(m => m.Id == j.UserId).FirstOrDefault();
                                var img = await _context.Photos.Where(m => m.AdvertId == j.AdvertId && m.IsMain == true).FirstOrDefaultAsync();

                                al.Add(new AdvertsListVievModel { Address = h, Advert = j, User = user, Photo = img });
                            }

                        }
                        return al;

                    }
                    else //0,0,0
                    {

                        var adv = await _context.Adverts.ToArrayAsync();
                        var al = new List<AdvertsListVievModel>();
                        foreach (var i in adv)
                        {
                            var ads = await _context.Addresses.Where(x => x.AdressId == i.AddressId).FirstOrDefaultAsync();
                            var us = await _context.Users.Where(x => x.Id == i.UserId).FirstOrDefaultAsync();
                            var img = await _context.Photos.Where(x => x.AdvertId == i.AdvertId && x.IsMain == true)
                                .FirstOrDefaultAsync();
                            al.Add(new AdvertsListVievModel { Address = ads, Advert = i, User = us, Photo = img });
                        }

                        return al;

                    }
                }

            }
        }


        public async Task<IEnumerable<AdvertsListVievModel>> AdvertsListAsync()
        {
            var adv = await _context.Adverts.Where(x=>x.Deactivate==false).ToArrayAsync();
            var al = new List<AdvertsListVievModel>();
            foreach (var i in adv)
            {
                var ads = await _context.Addresses.Where(x => x.AdressId == i.AddressId).FirstOrDefaultAsync();
                var us = await _context.Users.Where(x => x.Id == i.UserId).FirstOrDefaultAsync();
                var img = await _context.Photos.Where(x => x.AdvertId == i.AdvertId && x.IsMain == true)
                    .FirstOrDefaultAsync();
               al.Add(new AdvertsListVievModel{Address = ads, Advert = i, User = us, Photo = img});
            }

            return al;
        }

        public async Task<IEnumerable<AdvertsListVievModel>> DeactivateAdvertsListAsync()
        {

            var adv = await _context.Adverts.Where(x=>x.Deactivate==true).ToArrayAsync();
            var al = new List<AdvertsListVievModel>();
            foreach (var i in adv)
            {
                var ads = await _context.Addresses.Where(x => x.AdressId == i.AddressId).FirstOrDefaultAsync();
                var us = await _context.Users.Where(x => x.Id == i.UserId).FirstOrDefaultAsync();
                var img = await _context.Photos.Where(x => x.AdvertId == i.AdvertId && x.IsMain == true)
                    .FirstOrDefaultAsync();
                al.Add(new AdvertsListVievModel { Address = ads, Advert = i, User = us, Photo = img });
            }

            return al;

        }

        public async Task<AdvertsListVievModel> AdvertDetail(int id)
        {
            var adv = await _context.Adverts.Where(x => x.AdvertId == id).FirstOrDefaultAsync();
            var ads = await _context.Addresses.Where(x => x.AdressId == adv.AddressId).FirstOrDefaultAsync();
            var us = await _context.Users.Where(x => x.Id == adv.UserId).FirstOrDefaultAsync();
            var img = await _context.Photos.Where(x => x.AdvertId == adv.AdvertId && x.IsMain == true)
                .FirstOrDefaultAsync();


            return new AdvertsListVievModel {Address = ads, Advert = adv, User = us, Photo = img};
        }
        public async Task<bool> DeactivateAdvert(int ID)
        {
            var adv = await _context.Adverts.Where(m => m.AdvertId == ID).FirstOrDefaultAsync();
            _context.Entry(adv).Property(a => a.Deactivate).CurrentValue = true;
            _context.SaveChanges();

            return adv.Deactivate;

        }

        public async Task<bool> ActivateAdvert(int ID)
        {
            var adv = await _context.Adverts.Where(m => m.AdvertId == ID).FirstOrDefaultAsync();
            _context.Entry(adv).Property(a => a.Deactivate).CurrentValue = false;
            _context.SaveChanges();

            return !adv.Deactivate;

        }

        public async Task<bool> DeactivateAllUsersAdvert(string ID)
        {

            var items = await _context.Adverts
            .Where(x => x.UserId.Equals(ID))
            .ToArrayAsync();

            foreach (var i in items)
            {
                _context.Entry(i).Property(a => a.Deactivate).CurrentValue = true;

            }

            var result = await _context.SaveChangesAsync();

            return result == 1;


        }

        public async Task<bool> DeleteAdvertAsync(int ID)
        {
            var advert = new Advert { AdvertId = ID };
            _context.Adverts.Attach(advert);
            _context.Adverts.Remove(advert);


            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteAllAdvertsOfUserAsync(string ID)
        {
            var items = await _context.Adverts
           .Where(x => x.UserId.Equals(ID))
           .ToArrayAsync();

            foreach (var i in items)
            {
                _context.Adverts.Attach(i);
                _context.Adverts.Remove(i);

            }

            var result = await _context.SaveChangesAsync();

            return result == 1;
        }


    }
}
