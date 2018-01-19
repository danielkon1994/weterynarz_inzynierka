using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Doctor
{
    public class DoctorGraphicItem : BaseViewModel<int>
    {
        public int GraphicId { get; set; }

        public DateTime AvailableFrom { get; set; }

        public DateTime AvailableTo { get; set; }
    }
}
