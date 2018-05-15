using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Domain.Repositories.Interfaces;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Domain.ViewModels.Doctor;
using Weterynarz.Web.Models.NotifyMessage;
using Microsoft.Extensions.Logging;
using Weterynarz.Basic.Resources;
using Microsoft.AspNetCore.Authorization;
using Weterynarz.Basic.Const;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class DoctorGraphicsController : AdminBaseController
    {
        private IDoctorGraphicsRepository _doctorGraphicsRepository;
        private ILogger<DoctorGraphicsController> _logger;

        public DoctorGraphicsController(IDoctorGraphicsRepository doctorGraphicsRepository, ILogger<DoctorGraphicsController> logger)
        {
            _doctorGraphicsRepository = doctorGraphicsRepository;
            _logger = logger;
        }

        public IActionResult ShowGraphic(string doctorId)
        {
            if(string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorDoctorNotFound, MessageStatus.error);
                return new EmptyResult();
            }

            DoctorShowGraphicViewModel model = _doctorGraphicsRepository.GetDoctorGraphicToShowViewModel(doctorId);
            if(model == null)
            {
                base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorGraphicNotFound, MessageStatus.error);
                return new EmptyResult();
            }

            return PartialView("_ShowGraphic", model);
        }

        public async Task<IActionResult> ShowGraphics(string doctorId, int page = 1)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorDoctorNotFound, MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            var listElements = _doctorGraphicsRepository.GetAllGraphicsForDoctorViewModel(doctorId).OrderBy(a => a.CreationDate);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            ViewBag.DoctorId = doctorId;

            return View(model);
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Worker)]
        public IActionResult CreateGraphic(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorDoctorNotFound, MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            DoctorGraphicManageViewModel model = new DoctorGraphicManageViewModel()
            {
                DoctorId = doctorId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGraphic(DoctorGraphicManageViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _doctorGraphicsRepository.CreateNew(model);

                    Message message = new Message
                    {
                        OptionalText = ResAdmin.doctorGraphic_successAddGraphic,
                        Text = "Jeeessstt",
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);

                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId});
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ResAdmin.doctorGraphic_errorAddGraphic);
                    base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorAddGraphic, MessageStatus.error);
                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId });
                }
            }

            return View(model);
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Worker)]
        public async Task<IActionResult> EditGraphic(int id, string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorDoctorNotFound, MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            DoctorGraphicManageViewModel model = await _doctorGraphicsRepository.GetEditGraphicViewModel(id, doctorId);
            if(model == null)
            {
                base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorDoctorNotFound, MessageStatus.error);
                return RedirectToAction("ShowGraphics", "DoctorGraphics", new { doctorId = doctorId});
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditGraphic(DoctorGraphicManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _doctorGraphicsRepository.Update(model);

                    Message message = new Message
                    {
                        Text = "Jeeessttt",
                        OptionalText = ResAdmin.doctorGraphic_successSaveGraphic,
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);

                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ResAdmin.doctorGraphic_errorSaveGraphic);
                    base.NotifyMessage("Upppsss !", ResAdmin.doctorGraphic_errorSaveGraphic, MessageStatus.error);
                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId });
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Worker)]
        public async Task<IActionResult> DeleteGraphic(int id, string doctorId)
        {
            Message message = null;

            try
            {
                await _doctorGraphicsRepository.Delete(id);

                message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = ResAdmin.doctorGraphic_successDeleteGraphic,
                    MessageStatus = MessageStatus.success
                };

                return Json(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ResAdmin.doctorGraphic_errorDeleteGraphic);
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = ResAdmin.doctorGraphic_errorDeleteGraphic,
                    MessageStatus = MessageStatus.error
                };
                return Json(message);
            }
        }
    }
}