using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PPG_projekt.Models;
using PPG_projekt.Models.AccountViewModels;
using PPG_projekt.Models.AdminViewModels;
using PPG_projekt.Models.Dictionary;
using PPG_projekt.Services;


namespace PPG_projekt.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IAdvertService _advertService;
        private readonly IAddressService _addressService;
        private readonly IDictionaryService _dictionarySerce;

        

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAdvertService advertService,
            IAddressService addressService,
            IUserService userService,
            IDictionaryService dictionaryService
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _advertService = advertService;
            _addressService = addressService;
            _userService = userService;
            _dictionarySerce = dictionaryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (model.Regimen)
                {
                    if (!model.PersonalData)
                    {
                        ViewBag.Message = "Musisz wyrazić zgodę na przetwarzanie danych osobowych";
                        return View(model);
                    }

                    var user = new AppUser { UserName = model.Email, Email = model.Email, Name=model.Name, Nip=model.Nip };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Home");
                    }
                    //AddErrors(result);
                }
                ViewBag.Message = "Nie zaackceptowano regulaminu";
                

            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {


                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var deactive = await _userService.IsUserDeactivate(model.Email);

                    if (deactive)
                    {
                        // konto zablokowane 

                        return View(model);
                    }

                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(HomeController.Index),"Home");
                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    ViewBag.Message = "Niepoprawne dane";
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> AddressesList()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var addresses = await _addressService.GetIncompleteAddressAsync(currentUser);
            
            var model = new AddressesListViewModel()
            {
                Addresses = addresses
            };
            return View(model);
        }

        public  IActionResult AddUserAddress()
        {
            
            var l = _dictionarySerce.GetDictionariesObject(1).Select(x=> new SelectListItem { Value=x.DictionaryObjectName,Text=x.DictionaryObjectName});

            

            var vm = new AddUserAddressViewModel()
            {

                Voivodeships = l
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAddress( AddUserAddressViewModel model)
        {
            var l = _dictionarySerce.GetDictionaryObjectList("Voivodeships");
            model.Voivodeships = l;
            if (ModelState.IsValid)
            {
                Address address = new Address { City = model.City, HouseNr = model.HouseNr, LocalNr = model.LocalNr, Phone = model.Phone, PostCode = model.PostCode, Street = model.Street, Voivodeship = model.Voivodeship };

                    var currentUser = await _userManager.GetUserAsync(User);
                    var result = await _addressService.AddAddressAsync(address,currentUser.Id);

                    if (result)
                    {
                    ViewBag.Message = "Dodano adres";
                    return View(model);
                    }
            }


            return View(model);
        }

        public IActionResult EditAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var result = await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    ViewBag.Message = "Zmieniono Hasło";
                    return View();
                }

            }
            ViewBag.Message = "Niepoprawne dane";
            return View(model);
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
                return RedirectToAction("AddressesList", "Account");
            }
            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var result = await _advertService.DeleteAdvertAsync(id);

            if (result)
            {
                return RedirectToAction("AdvertsList", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AdvertDetails(int id)
        {
            var advert = await _advertService.DetalisAdvert(id);

     
            return View(advert);

        }

        public async Task<IActionResult> AdvertsList()
        {
            
            var currentUser = await _userManager.GetUserAsync(User);
            var adverts = await _advertService.GetIncompleteAdvertAsync(currentUser.Id);

            var model = new AdvertsViewModel()
            {
                Adverts = adverts
            };
            return View(model);
        }

    }

    
}