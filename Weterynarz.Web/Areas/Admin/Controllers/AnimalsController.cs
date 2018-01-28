using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Animal;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.NotifyMessage;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class AnimalsController : AdminBaseController
    {
        private IAnimalRepository _animalsRepository;

        public AnimalsController(IAnimalRepository animalsRepository)
        {
            this._animalsRepository = animalsRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _animalsRepository.GetIndexViewModel().OrderBy(a => a.Name);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            return View(model);
        }

        public IActionResult FillAnimalList(string userId)
        {
            var animalSelectList = _animalsRepository.GetUserAnimalsSelectList(userId);
            return Json(animalSelectList);
        }

        public IActionResult Create()
        {
            AnimalManageViewModel model = _animalsRepository.GetCreateNewViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _animalsRepository.CreateNew(model);

                Message message = new Message
                {
                    Text = "Jeeessttt",
                    OptionalText = "Zwierzak został dodany",
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }

            model = _animalsRepository.GetAllSelectListProperties(model);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            AnimalManageViewModel model = _animalsRepository.GetEditViewModel(id);
            if (model == null)
            {
                base.NotifyMessage("Nie znaleziono zwierzaka", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalManageViewModel model)
        {
            if(ModelState.IsValid)
            { 
                try
                {
                    await _animalsRepository.Edit(model);

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
                    base.NotifyMessage("Wystąpił błąd podczas edycji", "Upppsss !", MessageStatus.error);
                    return RedirectToAction("Index");
                }
            }

            model = _animalsRepository.GetAllSelectListProperties(model);
            return View(model);
        }
        
        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _animalsRepository.Delete(id);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie usunięto",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                base.NotifyMessage("Wystąpił błąd podczas usuwania", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }
        }
    }
}