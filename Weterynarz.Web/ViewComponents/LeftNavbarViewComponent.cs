using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weterynarz.Web.ViewComponents
{
    public class LeftNavbarViewComponent : ViewComponent
    {
        public LeftNavbarViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
