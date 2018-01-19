using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Doctor
{
    public class DoctorShowAllGraphicsViewModel
    {
        public string DoctorId { get; set; }
        public IQueryable<DoctorGraphicItem> DoctorGraphics { get; set; }
    }
}
