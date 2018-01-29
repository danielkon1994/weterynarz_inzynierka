using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Weterynarz.Basic.Resources;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Visit;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Web.Models.NotifyMessage;

namespace Weterynarz.Web.Controllers
{
    public class VisitController : BaseController
    {
        private IVisitRepository _visitRepository;
        private IAccountsRepository _accountRepository;

        public VisitController(IVisitRepository visitRepository, IAccountsRepository accountsRepository)
        {
            _visitRepository = visitRepository;
            _accountRepository = accountsRepository;
        }

        public async Task<IActionResult> MakeVisit()
        {
            ApplicationUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = _accountRepository.GetById(User.Identity.GetUserId());
            }

            VisitMakeVisitViewModel model = await _visitRepository.GetMakeVisitViewModel(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeVisit(VisitMakeVisitViewModel model)
        {
            bool busyDate = _visitRepository.CheckVisitExists(model.VisitDate);
            if (busyDate)
            {
                ModelState.AddModelError("", ResWebsite.visitWithDateExistsError);
            }

            if(!User.Identity.IsAuthenticated && (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.ComparePassword)))
            {
                ModelState.AddModelError("", ResWebsite.visitPasswordEmptyError);
            }
                
            if (ModelState.IsValid)
            {
                await _visitRepository.InsertFromVisitFormAsync(model);

                Message message = new Message
                {
                    Text = "Jeeessttt",
                    OptionalText = "Wizyta została zapisana",
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("MakeVisit");
            }

            base.NotifyMessage("Nie udało się zapisać wizyty", "Upppsss !", MessageStatus.error);
            model = await _visitRepository.GetMakeVisitViewModel(model);
            return View(model);
        }
    }
}