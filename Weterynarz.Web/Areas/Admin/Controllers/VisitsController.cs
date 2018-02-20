using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Animal;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Web.Models.NotifyMessage;
using Weterynarz.Domain.ViewModels.Visit;
using Microsoft.Extensions.Logging;
using Weterynarz.Domain.ViewModels.SummaryVisit;
using Weterynarz.Basic.Resources;
using Microsoft.AspNet.Identity;
using Weterynarz.Basic.Const;
using Weterynarz.Web.Services;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class VisitsController : AdminBaseController
    {
        private IVisitRepository _visitsRepository;
        private ISummaryVisitRepository _summaryVisitRepository;
        private ILogger<VisitsController> _logger;
        private IEmailSender _emailSender;

        public VisitsController(IVisitRepository visitsRepository, ILogger<VisitsController> logger,
            ISummaryVisitRepository summaryVisitRepository, IEmailSender emailSender)
        {
            this._visitsRepository = visitsRepository;
            _summaryVisitRepository = summaryVisitRepository;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.Identity.GetUserId();
            var listElements = await _visitsRepository.GetIndexViewModel(userId);
            var listVisit = listElements.OrderBy(a => a.VisitDate);
            var model = await PagingList.CreateAsync(listVisit, 20, page);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            VisitManageViewModel model = await _visitsRepository.GetCreateNewViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _visitsRepository.CreateNew(model);

                Message message = new Message
                {
                    Text = "Jeeessttt",
                    OptionalText = ResAdmin.visit_successAddVisit,
                    MessageStatus = Models.NotifyMessage.MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }

            model = await _visitsRepository.GetCreateNewViewModel();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VisitManageViewModel model = await _visitsRepository.GetEditViewModel(id);
            if (model == null)
            {
                base.NotifyMessage("Upppsss !", ResAdmin.visit_errorVisitNotFound, MessageStatus.error);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VisitManageViewModel model)
        {
            try
            {
                await _visitsRepository.Edit(model);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = ResAdmin.visit_successSaveVisit,
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ResAdmin.visit_errorEditVisit);
                base.NotifyMessage("Upppsss !", ResAdmin.visit_errorEditVisit, MessageStatus.error);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Approved(int id)
        {
            var visit = _visitsRepository.GetById(id);
            if(visit?.Approved == true)
            {
                base.NotifyMessage("Upppsss !", ResAdmin.visit_errorVisitEarlyApproved, MessageStatus.warning);
                return RedirectToAction("Index");
            }

            try
            {
                await _visitsRepository.Approved(visit);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = ResAdmin.visit_successVisitApproved,
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ResAdmin.visit_errorVisitApproved);
                base.NotifyMessage("Upppsss !", ResAdmin.visit_errorVisitApproved, MessageStatus.error);
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _visitsRepository.Delete(id);

                Message message = new Message
                {
                    Text = "Sukces !",
                    OptionalText = ResAdmin.visit_successDeleteVisit,
                    MessageStatus = MessageStatus.success
                };
                base.NotifyMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ResAdmin.visit_errorDeleteVisit);
                base.NotifyMessage("Upppsss !", ResAdmin.visit_errorDeleteVisit, MessageStatus.error);
                return RedirectToAction("Index");
            }
        }

        public IActionResult SummaryVisit(int visitId)
        {
            var model = _summaryVisitRepository.GetIndexViewModel(visitId);
            if(model != null)
            {
                return View(model);
            }

            base.NotifyMessage("Upppsss !", ResAdmin.summaryVisit_errorNotFoundSummary, MessageStatus.error);
            return RedirectToAction("Index");
        }

        public IActionResult SummaryVisitPdf(int visitId)
        {
            var model = _summaryVisitRepository.GetIndexViewModel(visitId);
            if (model != null)
            {
                return new Rotativa.AspNetCore.ViewAsPdf("SummaryVisitPdf", model) { FileName = "test.pdf" };
            }

            base.NotifyMessage("Upppsss !", ResAdmin.summaryVisit_errorNotFoundSummary, MessageStatus.error);
            return RedirectToAction("Index");
        }

        public IActionResult CreateSummaryVisit(int visitId)
        {
            if(visitId == 0)
            {
                base.NotifyMessage("Upppsss !", ResAdmin.visit_errorVisitNotFound, MessageStatus.error);
                return RedirectToAction("Index");
            }

            var model = _summaryVisitRepository.GetCreateViewModel(visitId);
            if (model != null)
            {
                return View(model);
            }

            base.NotifyMessage("Upppsss !", ResAdmin.visit_errorVisitNotFound, MessageStatus.error);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSummaryVisit(SummaryVisitManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {                
                    await _summaryVisitRepository.CreateNew(model);

                    Message message = new Message
                    {
                        Text = "Jeeessttt",
                        OptionalText = ResAdmin.summaryVisit_successAddSummary,
                        MessageStatus = Models.NotifyMessage.MessageStatus.success
                    };
                    base.NotifyMessage(message);

                    try
                    {
                        string summaryVisitUrl = Url.Action("SummaryVisit", "Visits", new { area = AreaNames.Admin, visitId = model.VisitId }, this.HttpContext.Request.Scheme);
                        var visit = _visitsRepository.GetById(model.VisitId, new string[] { "Animal.Owner" });

                        string userEmail = string.Empty;
                        if (visit != null)
                        { 
                            userEmail = visit.Animal.Owner.Email;
                        }

                        await _emailSender.SendEmailAsync(userEmail, "Dodano podsumowanie wizyty",
                           $"Dodano podsumowanie wizyty. Aby zobaczyć naciśnij: <a href='{summaryVisitUrl}'>Link</a>");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ResAdmin.visitSummary_errorSendEmail);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ResAdmin.summaryVisit_errorAddSummary);
                    base.NotifyMessage("Upppsss !", ResAdmin.summaryVisit_errorAddSummary, MessageStatus.error);
                }

                return RedirectToAction("Index");
            }

            model = _summaryVisitRepository.GetCreateViewModel(model.VisitId, model);
            return View(model);
        }
    }
}