using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            SiteContent = new Dictionary<string, string>();
        }

        public Dictionary<string, string> SiteContent { get; set; }

        public HomeContactViewModel ContactForm { get; set; }
    }
}
