using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Visit
{
    public class VisitIndexViewModel : BaseViewModel<int>
    {
        public DateTime VisitDate { get; set; }
        public string Owner { get; set; }
        public string Animal { get; set; }
        public string Doctor { get; set; }
        public bool Approved { get; set; }
        public int? SummaryId { get; set; }
    }
}
