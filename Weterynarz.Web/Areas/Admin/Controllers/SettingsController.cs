﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Settings;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class SettingsController : AdminBaseController
    {
        private ISettingsContentRepository _settingsContentRepository;

        public SettingsController(ISettingsContentRepository settingsContentRepository)
        {
            _settingsContentRepository = settingsContentRepository;
        }

        public IActionResult Content()
        {
            SettingsContentViewModel model = _settingsContentRepository.GetSettingsContentViewModel();
            return View(model);
        }

        public IActionResult ManageContent(int id)
        {
            SettingsContentManageViewModel model = _settingsContentRepository.GetSettingsContentManageViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageContent(SettingsContentManageViewModel model)
        {
            try
            {
                model.ModificationDate = DateTime.Now;
                await _settingsContentRepository.SaveSettingsContent(model);

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