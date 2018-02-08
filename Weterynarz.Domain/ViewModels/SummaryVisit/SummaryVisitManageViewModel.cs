using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.ViewModels.SummaryVisit
{
    public class SummaryVisitManageViewModel : BaseViewModel<int>
    {
        public int VisitId { get; set; }

        [Display(Name = "Choroby")]
        public IEnumerable<int> DiseaseIds { get; set; }
        public IEnumerable<SelectListItem> DiseaseSelectList { get; set; }

        [Display(Name = "Badania")]
        public IEnumerable<int> MedicalExaminationIds { get; set; }
        public IEnumerable<SelectListItem> MedicalExaminationSelectList { get; set; }

        [Display(Name = "Leki")]
        public string Drugs { get; set; }

        [Required(ErrorMessage = "Pole opis jest wymagany")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
