using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weterynarz.Web.ViewComponents
{
    public class TopNavbarViewComponent : ViewComponent
    {
        public TopNavbarViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
