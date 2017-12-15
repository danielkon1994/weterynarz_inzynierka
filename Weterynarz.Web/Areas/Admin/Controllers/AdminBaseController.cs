using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Weterynarz.Web.Controllers;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminBaseController : BaseController
    {
        public AdminBaseController()
        {

        }
    }
}