using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Weterynarz.Domain.ViewModels.Doctor;

namespace Weterynarz.Domain.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            SiteContent = new Dictionary<string, string>();
            Doctors = new Collection<DoctorHomeItem>();
        }

        public Dictionary<string, string> SiteContent { get; set; }

        public ICollection<DoctorHomeItem> Doctors { get; set; }

        public HomeContactViewModel ContactForm { get; set; }
    }
}
