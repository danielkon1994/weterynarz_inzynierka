using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Services.ViewModels.Settings;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Web.Models.NotifyMessage;

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

        public IActionResult ManageContent(int id)
        {
            SettingsContentManageViewModel model = _settingsContentService.GetSettingsContentManageViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageContent(SettingsContentManageViewModel model)
        {
            try
            {
                model.ModificationDate = DateTime.Now;
                await _settingsContentService.SaveSettingsContent(model);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie zapisano zmiany",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Content");
            }
            catch (Exception)
            {
                base.NotifyMessage("Wystąpił błąd podczas edycji", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Content");
            }
        }
    }
}