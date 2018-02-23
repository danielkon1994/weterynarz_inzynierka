using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Visit
{
    public class VisitReportViewModel
    {
        public string Animal { get; set; }
        public string Owner { get; set; }
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }
    }
}
