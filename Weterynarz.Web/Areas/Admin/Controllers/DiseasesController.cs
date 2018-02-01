using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Animal;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Domain.ViewModels.Disease;
using Microsoft.Extensions.Logging;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class DiseasesController : AdminBaseController
    {
        private IDiseasesRepository _diseasesRepository;
        private ILogger<DiseasesController> _logger;

        public DiseasesController(IDiseasesRepository diseasesRepository, ILogger<DiseasesController> logger)
        {
            this._diseasesRepository = diseasesRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _diseasesRepository.GetIndexViewModel().OrderBy(a => a.Name);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            return View(model);
        }

        public IActionResult Create()
        {
            DiseaseManageViewModel model = new DiseaseManageViewModel();
            model.Active = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiseaseManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _diseasesRepository.CreateNew(model);

                Message message = new Message
                {
                    Text = "Jeeessttt",
                    OptionalText = "Choroba została dodana",
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            DiseaseManageViewModel model = _diseasesRepository.GetEditViewModel(id);
            if (model == null)
            {
                base.NotifyMessage("Nie znaleziono choroby", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiseaseManageViewModel model)
        {
            try
            {
                await _diseasesRepository.Edit(model);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie zapisano",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas edycji");
                base.NotifyMessage("Wystąpił błąd podczas edycji", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }
        }
        
        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _diseasesRepository.Delete(id);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie usunięto",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas usuwania");
                base.NotifyMessage("Wystąpił błąd podczas usuwania", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }
        }
    }
}