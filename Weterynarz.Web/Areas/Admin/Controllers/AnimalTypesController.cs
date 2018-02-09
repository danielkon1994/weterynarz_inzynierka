using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Domain.ViewModels.AnimalTypes;
using Weterynarz.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class AnimalTypesController : AdminBaseController
    {
        private readonly IAnimalTypesRepository _animalTypesRepository;
        private ILogger<AnimalTypesController> _logger;

        public AnimalTypesController(IAnimalTypesRepository animalTypesRepository, ILogger<AnimalTypesController> logger)
        {
            this._animalTypesRepository = animalTypesRepository;
            _logger = logger;
        }

        // GET: AnimalTypes
        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _animalTypesRepository.GetIndexViewModel().OrderBy(a => a.Name);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            return View(model);
        }

        // GET: AnimalTypes/Create
        public IActionResult Create()
        {
            AnimalTypesManageViewModel model = new AnimalTypesManageViewModel();

            return View(model);
        }

        // POST: AnimalTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalTypesManageViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _animalTypesRepository.CreateNewType(model);

                Message message = new Message
                {
                    Text = "Jeeessttt",
                    OptionalText = "Typ został dodany",
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: AnimalTypes/Edit/5
        public IActionResult Edit(int id)
        {
            AnimalTypesManageViewModel model = _animalTypesRepository.GetEditViewModel(id);
            if (model == null)
            {
                base.NotifyMessage("Nie znaleziono typu z takim identyfikatorem", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // POST: AnimalTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalTypesManageViewModel model)
        {
            try
            {
                bool result = await _animalTypesRepository.EditType(model);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie zapisano typ",
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

        // GET: AnimalTypes/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _animalTypesRepository.DeleteType(id);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie usunięto typ",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas usuwania");
                base.NotifyMessage("Wystąpił błąd podczas usuwania", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }
        }
    }
}