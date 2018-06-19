using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPG_projekt.Models;
using PPG_projekt.Models.AccountViewModels;
using PPG_projekt.Models.AdminViewModels;
using PPG_projekt.Models.AdvertViewModels;
using PPG_projekt.Models.Dictionary;
using PPG_projekt.Services;

namespace PPG_projekt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IAdvertService _advertService;
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;


        public AdminController(
            UserManager<AppUser> userManager,

            IAdvertService advertService,
            IAddressService addressService,
            IUserService userService,
            IDictionaryService dictionaryService
           )
        {
            _userManager = userManager;

            _advertService = advertService;
            _addressService = addressService;
            _userService = userService;
            _dictionaryService = dictionaryService;
        }



        public async Task<IActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Administrator");
            var everyone = await _userManager.Users.ToArrayAsync();


            return View();
        }

        //Address

        public async Task<IActionResult> AddressesList(bool activ)
        {
            if (activ)
            {
                var addresses = await _addressService.GetIncompleteAddressAsync();
                var model = new AddressesViewModel()
                {
                    Addresses = addresses
                };
                return View(model);
            }
            else
            {
                var addresses = await _addressService.GetIncompleteDesactivateAddressAsync();
                var model = new AddressesViewModel()
                {
                    Addresses = addresses
                };
                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddressesList(string searchString)
        {
            var addresses = await _addressService.Search(searchString);

            var model = new AddressesViewModel()
            {
                Addresses = addresses
            };
            return View(model);
        }

        public async Task<IActionResult> DeactivateAddress(int id)
        {
            var result = await _addressService.DeactivateAddressAsync(id);

            if (result)
            {
                return RedirectToAction("AddressesList", "Admin", new {activ=true });
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> ActivateAddress(int id)
        {
            var result = await _addressService.ActivateAddressAsync(id);

            if (result)
            {
                return RedirectToAction("AddressesList", "Admin", new { activ = false });
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> AddressDetails(int id)
        {
            var address = await _addressService.DetalisAddress(id);


            return View(address);

        }

        public async Task<IActionResult> DeleteAddress(int id)
        {
            var result = await _addressService.DeleteAddressAsync(id);

            if (result)
            {
                return RedirectToAction("AddressesList", "Admin", new { activ = true });
            }
            return RedirectToAction("Index", "Home");

        }

        //USERS

        public async Task<IActionResult> UsersList(bool activ)
        {
            
            if (activ)
            {
                var users = await _userService.GetIncompleteUserAsync();

                var model = new UsersViewModel()
                {
                    Users = users
                };
                return View(model);
            }
            else
            {
                var users = await _userService.GetIncompleteDeactivateUserAsync();

                var model = new UsersViewModel()
                {
                    Users = users
                };
                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UsersList(string searchString)
        {

            var users = await _userService.Search(searchString);

            var model = new UsersViewModel()
            {
                Users = users
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {

            var user = await _userService.FindUser(id);

            if (user != null)
            {


                var addressResult = await _addressService.DeleteAllAddressesOfUserAsync(user.Id);

                var advertResult = await _advertService.DeleteAllAdvertsOfUserAsync(user.Id);

            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UsersList", "Admin",new { activ=true});
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> DeactivateUser(string id)
        {

            var user = await _userService.FindUser(id);

            if (user != null)
            {


                var addressResult = await _addressService.DeactivateAllUserAddressAsync(user.Id);

                var advertResult = await _advertService.DeactivateAllUsersAdvert(user.Id);

            }

            var result = await _userService.DeactivateUser(user.Id);

            if (result)
            {
                return RedirectToAction("UsersList", "Admin",new {activ=true });
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> ActivateUser(string id)
        {
            var result = await _userService.ActivateUser(id);

            if (result)
            {
                return RedirectToAction("UsersList", "Admin", new { activ=false});
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userService.FindUser(id);


            return View(user);

        }

        //Adverts

        public async Task<IActionResult> AdvertsList(bool activ)
        {

            IEnumerable<AdvertsListVievModel> advertsList;

            if (activ)
            {
                advertsList = await _advertService.AdvertsListAsync();

            }
            else
            {
               advertsList = await _advertService.DeactivateAdvertsListAsync();
            }

            return View(advertsList);
        }

        [HttpPost]
        public async Task<IActionResult> AdvertsList(string searchString)
        {
            var adverts = await _advertService.Search(searchString);

            var model = new AdvertsViewModel()
            {
                Adverts = adverts
            };
            return View(model);
        }

        public async Task<IActionResult> AdvertDetails(int id)
        {
            var advert = await _advertService.AdvertDetail(id);
            return View(advert);

        }

        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var result = await _advertService.DeleteAdvertAsync(id);

            if (result)
            {
                return RedirectToAction("AdvertsList", "Admin",new { activ=true});
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> DeactivateAdvert(int id)
        {

            var result = await _advertService.DeactivateAdvert(id);

            if (result)
            {
                return RedirectToAction("AdvertsList", "Admin",new { activ=true});
            }
            return RedirectToAction("Index", "Home");


        }

        public async Task<IActionResult> ActivateAdvert(int id)
        {

            var result = await _advertService.ActivateAdvert(id);

            if (result)
            {
                return RedirectToAction("AdvertsList", "Admin",new { activ=false});
            }
            return RedirectToAction("Index", "Home");


        }

        //Dictionary

        public IActionResult DictionariesList(int i,bool activ)
        {

            var dictionary = _dictionaryService.GetDictionaries();
            IEnumerable<DictionaryObject> v;

            if (activ) {
                v = _dictionaryService.GetDictionariesObject(i);
            }
            else
            {
                v = _dictionaryService.GetDeactiveDictionariesObject(i);
            }
            var model = new DictionaryView()
            {
                Dictionaries = dictionary,
                DictionaryObjects =v,
                Page=i
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DictionariesList(int i,string search)
        {

            var dictionary = _dictionaryService.GetDictionaries();
            var dictionaryObject = _dictionaryService.SearchObject(i, search);
            var model = new DictionaryView()
            {
                Dictionaries = dictionary,
                DictionaryObjects = dictionaryObject,
                Page = i
            };
            return View(model);
        }

        public IActionResult DeleteDictionary(int id,int v)
        {
            var result = _dictionaryService.DeleteDictionary(id);

            if (result)
            {
                return RedirectToAction("DictionariesList", "Admin", new { i = v,activ=true });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddDictionary(string name)
        {

            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Home");
            }

            var result = _dictionaryService.AddDictionary(name);

            if (result)
            {
                return RedirectToAction("DictionariesList", "Admin", new { i = 1 });
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteDictionaryObject(int id,int v)
        {

            var result = _dictionaryService.DeleteDictionaryObject(id);

            if (result)
            {
                return RedirectToAction("DictionariesList", "Admin", new { i = v ,activ=true});
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult AddDictionaryObject(string name, int ID)
        {

            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Home");
            }

            var result = _dictionaryService.AddDictionaryObject(ID,name);

            if (result)
            {
                return RedirectToAction("DictionariesList", "Admin", new { i = ID , activ=true});
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ActivateDictionaryObject(int id,int v)
        {
            var result = _dictionaryService.ActiveDictionaryObject(id);

            if (result)
            {
                return RedirectToAction("DictionariesList", "Admin", new { i = v,activ=false });
            }
            return RedirectToAction("Index", "Home");

        }

        //DictionarySubObj
        public IActionResult SubObjList(int i, bool activ)
        {

            var dictionaryObj = _dictionaryService.GetDictionaryObject();
            IEnumerable<DictionarySubObject> v;

            if (activ)
            {
                v = _dictionaryService.GetDictionariesSubObject(i);
            }
            else
            {
                v = _dictionaryService.GetDeactivateDictionarySubObjects(i);
            }

            var model = new DictionarySubObjView()
            {
                DictionaryObjects = dictionaryObj,
                DictionarySubObj=v,
                Page = i
            };

            return View(model);

        }

        public IActionResult AddSubObj(string name,int ID)
        {

            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Home");
            }

            var result = _dictionaryService.AddDictionarySubObject(ID, name);

            if (result)
            {
                return RedirectToAction("SubObjList", "Admin", new { i = ID, activ = true });
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult DeleteSubObject(int id, int v)
        {
            var result = _dictionaryService.DeleteDictionarySubObject(id);

            if (result)
            {
                return RedirectToAction("SubObjList", "Admin", new { i = v, activ = true });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ActivateSubObject(int id, int v)
        {
            var result = _dictionaryService.ActiveDictionarySubObject(id);

            if (result)
            {
                return RedirectToAction("SubObjList", "Admin", new { i = v, activ = false });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DictionaryObjEdit(int ID)
        {

            var obj = _dictionaryService.GetDictionaryObject(ID);

            var model = new DictionaryObjEdit()
            {
                Dictionaries = _dictionaryService.GetDictionariestList(),
                Name = obj.DictionaryObjectName,
                SubObj = obj.SubObj,
                DictionaryObjID=ID
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DictionaryObjEdit(DictionaryObjEdit obj)
        {

            var dictObj = _dictionaryService.DictionaryObjEdit(obj);

            var model = new DictionaryObjEdit()
            {
                Dictionaries = _dictionaryService.GetDictionariestList(),
                DictionaryID=dictObj.DictionaryId.ToString(),
                DictionaryObjID=dictObj.DictionaryObjectId,
                Name=dictObj.DictionaryObjectName,
                SubObj=dictObj.SubObj
                
            };
            
           
            return View(model);

        }

        public IActionResult DictionarySubObjEdit(int ID)
        {

            var obj = _dictionaryService.GetDictionarySubObject(ID);
            var DictObj = _dictionaryService.GetDictionaryObject(obj.DictionaryObjectId);
            var Dic = _dictionaryService.GetDictionary(DictObj.DictionaryId);
            var model = new DictionarySubObjEdit()
            {
                DictionaryObjects = _dictionaryService.GetDictionaryObjectList(Dic.DictionaryName),
                Name=obj.DictionarySubObjectName,
                DictionarySubObjID=obj.DictionarySubObjectId

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DictionarySubObjEdit(DictionarySubObjEdit obj)
        {
            var dictSubObj = _dictionaryService.DictionarySubObjEdit(obj);
            var DictObj = _dictionaryService.GetDictionaryObject(dictSubObj.DictionaryObjectId);
            var Dic = _dictionaryService.GetDictionary(DictObj.DictionaryId);
            var model = new DictionarySubObjEdit()
            {
                DictionaryObjects = _dictionaryService.GetDictionaryObjectList(Dic.DictionaryName),
                DictionaryObjID = dictSubObj.DictionaryObjectId.ToString(),
                DictionarySubObjID = dictSubObj.DictionarySubObjectId,
                Name = dictSubObj.DictionarySubObjectName

            };

            return View(model);
        }
    }
}