using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Services.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            SiteContent = new Dictionary<string, string>();
        }

        public Dictionary<string, string> SiteContent { get; set; }
    }
}
