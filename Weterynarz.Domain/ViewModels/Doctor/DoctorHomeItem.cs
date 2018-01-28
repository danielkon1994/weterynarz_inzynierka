using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Doctor
{
    public class DoctorHomeItem
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public DoctorShowGraphicViewModel Graphic { get; set; }

        public string DisplayName {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
