﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Content()
        {
            return View();
        }
    }
}