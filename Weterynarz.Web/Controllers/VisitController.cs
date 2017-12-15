using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Weterynarz.Web.Controllers
{
    public class VisitController : BaseController
    {
        public IActionResult MakeVisit()
        {
            return View();
        }
    }
}