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
        public IActionResult Contact(HomeIndexViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _emailSender.SendEmailAsync(model.ContactForm.Email, "Kontakt ze strony", model.ContactForm.Message);
                    return Json(new JsonResponseModel { Message = "Wiadomość została wysłana", Status = MessageStatus.success });
                }
                catch(Exception)
                {
                    return Json(new JsonResponseModel { Message = "Coś poszło nie tak przy wysyłaniu wiadomości", Status = MessageStatus.error });
                }
            }

            return Json(new JsonResponseModel { Message = "Nie udało się wysłać wiadomości", Status = MessageStatus.error });
        }
        
    }
}
