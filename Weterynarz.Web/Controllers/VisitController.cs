using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Weterynarz.Basic.Resources;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Visit;

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
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
            }

            VisitMakeVisitViewModel model = await _visitRepository.GetMakeVisitViewModel(userId);

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
            if(User.Identity.IsAuthenticated)
            {
                var user = _accountRepository.GetById(User.Identity.GetUserId());
                if(user != null)
                {
                    model.UserName = user.UserName;
                    model.Email = user.Email;
                }
            }
            //if (model.AnimalId == 0)
            //{
            //    ModelState.AddModelError("", ResWebsite.visitAnimalIdNullError);
            //}

            if (ModelState.IsValid)
            {
                await _visitRepository.InsertNewVisit(model);
            }

            model = await _visitRepository.GetMakeVisitViewModel(model);

            return View(model);
        }
    }
}