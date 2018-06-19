using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PPG_projekt.Models;
using PPG_projekt.Models.AccountViewModels;
using PPG_projekt.Models.AdvertViewModels;
using PPG_projekt.Models.IndexViewModels;
using PPG_projekt.Services;

namespace PPG_projekt.Controllers
{
    public class AdvertController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdvertService _advertService;
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
    //    private readonly IPhotoService _photoService;


        public AdvertController(
            UserManager<AppUser> userManager,

            IAdvertService advertService,
            IAddressService addressService,
            IUserService userService
           // IPhotoService photoService
        )
        {
            _userManager = userManager;
            
            _advertService = advertService;
            _addressService = addressService;
            _userService = userService;
          //  _photoService = photoService;

        }

        public async Task<IActionResult> Lista_ogloszen()
        {
            var advertsList = await _advertService.AdvertsListAsync();

            return View(advertsList);

        }
        public async Task<IActionResult> Ogloszenie(int id)
        {
            var advert = await _advertService.AdvertDetail(id);
            return View(advert);

        }

        [HttpPost]
        public async Task<IActionResult> Lista_ogloszen(SearchModel s)
        {


            var adverts = await _advertService.Search(s);

            //var model = new AdvertsListViewModel()
            //{
            //    Adverts = adverts
            //};
            return View(adverts);
        }


    }
}
