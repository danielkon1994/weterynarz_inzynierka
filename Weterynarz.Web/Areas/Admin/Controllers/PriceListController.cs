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
using Weterynarz.Domain.ViewModels.PriceList;
using Weterynarz.Basic.Const;
using Microsoft.AspNetCore.Authorization;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class PriceListController : AdminBaseController
    {
        private IPriceListRepository _priceListRepository;
        private IMedicalExaminationTypesRepository _medicalExaminationTypesRepository;
        private ILogger<PriceListController> _logger;

        public PriceListController(IPriceListRepository priceListRepository,
            IMedicalExaminationTypesRepository medicalExaminationTypesRepository,
            ILogger<PriceListController> logger)
        {
            this._priceListRepository = priceListRepository;
            this._medicalExaminationTypesRepository = medicalExaminationTypesRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _priceListRepository.GetIndexViewModel().OrderBy(a => a.Name);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            return View(model);
        }

        public IActionResult Create()
        {
            PriceListManageViewModel model = new PriceListManageViewModel();
            model.MedicalExaminationSelectList = _medicalExaminationTypesRepository.GetMedicalExaminationSelectList();
            model.Active = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceListManageViewModel model)
        {
            if(model.Type != Basic.Enum.PriceListEntryType.Examination)
            {
                ModelState.Remove("SelectedMedicalExaminationId");
            }

            if (ModelState.IsValid)
            {
                await _priceListRepository.CreateNew(model);

                Message message = new Message
                {
                    Text = "Jeeessttt",
                    OptionalText = "Cena została dodana",
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }

            model.MedicalExaminationSelectList = _medicalExaminationTypesRepository.GetMedicalExaminationSelectList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            PriceListManageViewModel model = _priceListRepository.GetEditViewModel(id);
            if (model == null)
            {
                base.NotifyMessage("Nie znaleziono ceny", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }
            model.MedicalExaminationSelectList = _medicalExaminationTypesRepository.GetMedicalExaminationSelectList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PriceListManageViewModel model)
        {
            if (model.Type != Basic.Enum.PriceListEntryType.Examination)
            {
                ModelState.Remove("SelectedMedicalExaminationId");
            }

            if (ModelState.IsValid)
            { 
                try
                {
                    await _priceListRepository.Edit(model);

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

            model.MedicalExaminationSelectList = _medicalExaminationTypesRepository.GetMedicalExaminationSelectList();

            return View(model);
        }
        
        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _priceListRepository.Delete(id);

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