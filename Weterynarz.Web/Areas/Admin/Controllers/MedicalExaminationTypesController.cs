﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.MedicalExaminationTypes;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class MedicalExaminationTypesController : AdminBaseController
    {
        private readonly IMedicalExaminationTypesRepository _medicalExaminationTypesRepository;

        public MedicalExaminationTypesController(IMedicalExaminationTypesRepository medicalExaminationTypesRepository)
        {
            _medicalExaminationTypesRepository = medicalExaminationTypesRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var listElements = _medicalExaminationTypesRepository.GetIndexViewModel().OrderBy(a => a.Name);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            return View(model);
        }

        // GET: AnimalTypes/Create
        public IActionResult Create()
        {
            MedicalExaminationTypesManageViewModel model = new MedicalExaminationTypesManageViewModel();

            return View(model);
        }

        // POST: AnimalTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalExaminationTypesManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _medicalExaminationTypesRepository.CreateNewType(model);

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
            MedicalExaminationTypesManageViewModel model = _medicalExaminationTypesRepository.GetEditViewModel(id);
            if(model == null)
            {
                base.NotifyMessage("Nie znaleziono typu z takim identyfikatorem", "Upppsss !", MessageStatus.error);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // POST: AnimalTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalExaminationTypesManageViewModel model)
        {
            try
            {
                bool result = await _medicalExaminationTypesRepository.EditType(model);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie zapisano typ",
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
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
                bool result = await _medicalExaminationTypesRepository.DeleteType(id);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie usunięto typ",
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