using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPG_projekt.Models;
using PPG_projekt.Models.AdminViewModels;
using PPG_projekt.Models.IndexViewModels;
using PPG_projekt.Services;

namespace PPG_projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly IDictionaryService _dictionaryService;

        public HomeController(
            IAdvertService advertService,
            IDictionaryService dictionaryService)
        {
            _advertService = advertService;
            _dictionaryService = dictionaryService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var list = _dictionaryService.GetDictionaryObjectList("Categories");

            var vm = new SearchModel()
            {
                Categorys = list
            };

            return View(vm);
            //return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            var list = _dictionaryService.GetDictionaryObjectList(model.Category);

            model.Categorys = list;

            return View(model);
        }

        public ActionResult Lista_ogloszen()
        {
            return View();
        }

    }
}
