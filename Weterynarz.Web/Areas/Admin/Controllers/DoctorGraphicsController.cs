using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Domain.Repositories.Interfaces;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Domain.ViewModels.Doctor;
using Weterynarz.Web.Models.NotifyMessage;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class DoctorGraphicsController : AdminBaseController
    {
        private IDoctorGraphicsRepository _doctorGraphicsRepository;

        public DoctorGraphicsController(IDoctorGraphicsRepository doctorGraphicsRepository)
        {
            _doctorGraphicsRepository = doctorGraphicsRepository;
        }

        public IActionResult ShowGraphic(string doctorId)
        {
            if(string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Nie wiadomo o jakiego lekarza chodzi", "Upppsss !", MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            DoctorShowGraphicViewModel model = _doctorGraphicsRepository.GetDoctorGraphicToShowViewModel(doctorId);
            if(model == null)
            {
                base.NotifyMessage("Nie udało się pobrać grafiku lekarza", "Upppsss !", MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            return PartialView("_ShowGraphic", model);
        }

        public async Task<IActionResult> ShowGraphics(string doctorId, int page = 1)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Nie wiadomo o jakiego lekarza chodzi", "Upppsss !", MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            var listElements = _doctorGraphicsRepository.GetAllGraphicsForDoctorViewModel().OrderBy(a => a.CreationDate);
            var model = await PagingList.CreateAsync(listElements, 20, page);

            ViewBag.DoctorId = doctorId;

            return View(model);
        }

        public IActionResult CreateGraphic(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Nie wiadomo o jakiego lekarza chodzi", "Upppsss !", MessageStatus.error);
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
                        OptionalText = "Grafik został dodany",
                        Text = "Jeeessstt",
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);

                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId});
                }
                catch (Exception)
                {
                    base.NotifyMessage("Wystąpił problem z dodaniem grafiku lekarza", "Upppsss !", MessageStatus.error);
                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId });
                }
            }

            return View(model);
        }

        public async Task<IActionResult> EditGraphic(int id, string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                base.NotifyMessage("Nie wiadomo o jakiego lekarza chodzi", "Upppsss !", MessageStatus.error);
                return RedirectToAction("ListDoctors", "Users");
            }

            DoctorGraphicManageViewModel model = await _doctorGraphicsRepository.GetEditGraphicViewModel(id, doctorId);

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
                        OptionalText = "Grafik został zapisany",
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);

                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId });
                }
                catch (Exception ex)
                {
                    base.NotifyMessage("Wystąpił problem z zapisem grafiku lekarza", "Upppsss !", MessageStatus.error);
                    return RedirectToAction("ShowGraphics", new { doctorId = model.DoctorId });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGraphic(int id, string doctorId)
        {
            Message message = null;

            try
            {
                await _doctorGraphicsRepository.Delete(id);

                message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = "Pomyślnie usunięto grafik",
                    MessageStatus = MessageStatus.success
                };

                return Json(message);
            }
            catch (Exception)
            {
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = "Coś poszło nie tak przy usuwaniu grafiki",
                    MessageStatus = MessageStatus.error
                };
                return Json(message);
            }
        }
    }
}