using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Services.ViewModels.Settings;
using Weterynarz.Services.Services.Interfaces;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class SettingsController : AdminBaseController
    {
        private ISettingsContentService _settingsContentService;

        public SettingsController(ISettingsContentService settingsContentService)
        {
            _settingsContentService = settingsContentService;
        }

        public IActionResult Content()
        {
            SettingsContentViewModel model = _settingsContentService.GetSettingsContentViewModel();
            return View(model);
        }
    }
}