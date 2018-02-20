using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Basic.Resources;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Visit;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Web.Models.NotifyMessage;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Weterynarz.Web.Services;
using Weterynarz.Basic.Const;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace Weterynarz.Web.Controllers
{
    public class VisitController : BaseController
    {
        private IVisitRepository _visitRepository;
        private IAccountsRepository _accountRepository;
        private ILogger<VisitController> _logger;
        private IEmailSender _emailSender;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public VisitController(IVisitRepository visitRepository, IAccountsRepository accountsRepository, 
            ILogger<VisitController> logger, IEmailSender emailSender, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _visitRepository = visitRepository;
            _accountRepository = accountsRepository;
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
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
            await checkUserFromMakeVisitFormAsync(model);

            bool busyDate = _visitRepository.CheckVisitExists(model.VisitDate);
            if (busyDate)
            {
                ModelState.AddModelError("", ResWebsite.visitWithDateExistsError);
            }

            if(User.Identity.IsAuthenticated)
            {
                ModelState.Remove("Password");
            }

            if(model.AnimalId != null)
            {
                ModelState.Remove("AnimalTypeId");
                ModelState.Remove("AnimalName");
                ModelState.Remove("AnimalBirthdate");
            }

            //if(!User.Identity.IsAuthenticated && (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.ComparePassword)))
            //{
            //    ModelState.AddModelError("", ResWebsite.visitPasswordEmptyError);
            //    ModelState.MarkFieldValid("Password");
            //}
                
            if (ModelState.IsValid)
            {
                try { 
                    var visit = await _visitRepository.InsertFromVisitFormAsync(model);

                    Message message = new Message
                    {
                        Text = "Jeeessttt",
                        OptionalText = ResWebsite.visitSaveVisitSuccess,
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);

                    try
                    {
                        string visitUrl = Url.Action("Index", "Visits", new { area = AreaNames.Admin}, this.HttpContext.Request.Path);
                        string userEmail = visit.Animal.Owner.Email;

                        await _emailSender.SendEmailAsync(userEmail, "Dodano wizytę",
                           $"Aby przejść do listy swoich wizyt naciśnij: <a href='{visitUrl}'>Link</a>");
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, ResWebsite.visitSendEmailError);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ResWebsite.visitCannotSaveVisitError);
                    Message message = new Message
                    {
                        Text = "Upppsss !",
                        OptionalText = ResWebsite.visitCannotSaveVisitError,
                        MessageStatus = Models.NotifyMessage.MessageStatus.error
                    };
                    base.NotifyMessage(message);
                }

                return RedirectToAction("MakeVisit");
            }

            model = await _visitRepository.GetMakeVisitViewModel(model);
            return View(model);
        }

        private async Task checkUserFromMakeVisitFormAsync(VisitMakeVisitViewModel model)
        {
            if(string.IsNullOrEmpty(model.UserId))
            { 
                var userNameExists = await _userManager.FindByNameAsync(model.UserName);
                if (userNameExists != null)
                {
                    if (userNameExists.UserName == model.UserName && userNameExists.Active)
                    {
                        ModelState.AddModelError("", "Użytkownik o takiej nazwie użytkownika już istnieje");
                    }
                }

                var userEmailExists = await _userManager.FindByEmailAsync(model.Email);
                if (userEmailExists != null)
                {
                    if (userEmailExists.Email == model.Email && userEmailExists.Active)
                    {
                        ModelState.AddModelError("", "Użytkownik z takim adresem email już istnieje");
                    }
                }
            }
        }
    }
}