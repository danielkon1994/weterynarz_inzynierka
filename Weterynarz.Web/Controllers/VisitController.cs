using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.Visit;
using Microsoft.AspNet.Identity;

namespace Weterynarz.Web.Controllers
{
    public class VisitController : BaseController
    {
        private IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        public async Task<IActionResult> MakeVisit()
        {
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
            }

            VisitMakeVisitViewModel model = await _visitService.GetMakeVisitViewModel(userId);

            return View(model);
        }
    }
}