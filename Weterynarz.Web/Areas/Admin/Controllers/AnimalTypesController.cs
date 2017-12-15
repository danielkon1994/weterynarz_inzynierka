using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.AnimalTypes;
using ReflectionIT.Mvc.Paging;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class AnimalTypesController : AdminBaseController
    {
        private readonly IAnimalTypesService _animalTypesService;

        public AnimalTypesController(IAnimalTypesService animalTypesService)
        {
            this._animalTypesService = animalTypesService;
        }

        // GET: AnimalTypes
        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _animalTypesService.GetIndexViewModel().OrderBy(a => a.Name);
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
                await _animalTypesService.CreateNewType(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: AnimalTypes/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimalTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimalTypes/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimalTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}