using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Web.Models;
using Weterynarz.Domain.ContextDb;
using Microsoft.Extensions.Caching.Memory;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Web.Services;
using Weterynarz.Domain.ViewModels.Home;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IHomeRepository _homeRepository;
        private IEmailSender _emailSender;

        public HomeController(IHomeRepository homeRepository, IEmailSender emailSender)
        {
            _homeRepository = homeRepository;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = _homeRepository.GetIndexViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(HomeIndexViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _emailSender.SendEmailAsync(model.ContactForm.Email, "Kontakt ze strony", model.ContactForm.Message);

                    Message message = new Message
                    {
                        Text = "Jeeessttt",
                        OptionalText = "Wiadomość została wysłana",
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    base.NotifyMessage("Coś poszło nie tak przy wysyłaniu wiadomości", "Upppsss !", MessageStatus.error);
                    return RedirectToAction("Index");
                }
            }

            base.NotifyMessage("Nie udało się wysłać wiadomości", "Upppsss !", MessageStatus.error);
            return RedirectToAction("Index");
        }
        
    }
}
