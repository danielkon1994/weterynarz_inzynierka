using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Animal;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Domain.ViewModels.Visit;
using Microsoft.Extensions.Logging;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class VisitsController : AdminBaseController
    {
        private IVisitRepository _visitsRepository;
        private ILogger<VisitsController> _logger;

        public VisitsController(IVisitRepository visitsRepository, ILogger<VisitsController> logger)
        {
            this._visitsRepository = visitsRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _visitsRepository.GetIndexViewModel().OrderBy(a => a.VisitDate);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            VisitManageViewModel model = await _visitsRepository.GetCreateNewViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _visitsRepository.CreateNew(model);

                Message message = new Message
                {
                    OptionalText = "Jeeessttt",
                    Text = "Wizyta została dodana",
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }

            model = await _visitsRepository.GetCreateNewViewModel();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VisitManageViewModel model = await _visitsRepository.GetEditViewModel(id);
            if (model == null)
            {
                base.NotifyMessage("Nie znaleziono wizyty", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VisitManageViewModel model)
        {
            try
            {
                await _visitsRepository.Edit(model);

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

        public async Task<IActionResult> Approved(int id)
        {
            var visit = _visitsRepository.GetById(id);
            if(visit?.Approved == true)
            {
                base.NotifyMessage("", "Wizyta została już wcześniej zatwierdzona", MessageStatus.warning);
                return RedirectToAction("Index");
            }

            try
            {
                await _visitsRepository.Approved(visit);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Zatwierdzono wizytę",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas zatwierdzania wizyty");
                base.NotifyMessage("Wystąpił błąd podczas zatwierdzania wizyty", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _visitsRepository.Delete(id);

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