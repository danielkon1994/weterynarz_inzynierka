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
using Microsoft.Extensions.Logging;
using Weterynarz.Basic.Const;
using Microsoft.AspNetCore.Authorization;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Worker)]
    public class MedicalExaminationTypesController : AdminBaseController
    {
        private readonly IMedicalExaminationTypesRepository _medicalExaminationTypesRepository;
        private ILogger<MedicalExaminationTypesController> _logger;

        public MedicalExaminationTypesController(IMedicalExaminationTypesRepository medicalExaminationTypesRepository, ILogger<MedicalExaminationTypesController> logger)
        {
            _medicalExaminationTypesRepository = medicalExaminationTypesRepository;
            _logger = logger;
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
                    OptionalText = "Badanie zostało dodane",
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
                base.NotifyMessage("Nie znaleziono badania z takim identyfikatorem", "Upppsss !", MessageStatus.error);
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
                    OptionalText = "Pomyślnie zapisano badanie",
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
                bool result = await _medicalExaminationTypesRepository.DeleteType(id);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie usunięto badanie",
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