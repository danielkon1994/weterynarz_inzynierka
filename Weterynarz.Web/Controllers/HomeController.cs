using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Web.Models;
using Weterynarz.Domain.ContextDb;
using Microsoft.Extensions.Caching.Memory;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.Home;

namespace Weterynarz.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = _homeService.GetIndexViewModel();

            return View(model);
        }
    }
}
