using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;
using Weterynarz.Domain.Extension;

namespace Weterynarz.Domain.ViewModels.SummaryVisit
{
    public class SummaryVisitManageViewModel : BaseViewModel<int>
    {
        public int VisitId { get; set; }

        [Display(Name = "summaryVisitManageViewModel_disease", ResourceType = typeof(ResAdmin))]
        public IEnumerable<int> DiseaseIds { get; set; }
        public IEnumerable<SelectListItem> DiseaseSelectList { get; set; }

        [Display(Name = "summaryVisitManageViewModel_medicalExamination", ResourceType = typeof(ResAdmin))]
        public IEnumerable<int> MedicalExaminationIds { get; set; }
        public IEnumerable<SelectListItem> MedicalExaminationSelectList { get; set; }

        [Display(Name = "summaryVisitManageViewModel_additionalCost", ResourceType = typeof(ResAdmin))]
        public IEnumerable<int> AdditionalCostIds { get; set; }
        public IEnumerable<ExtendSelectListItem> AdditionalCostsSelectList { get; set; }

        [Display(Name = "summaryVisitManageViewModel_price", ResourceType = typeof(ResAdmin))]
        public decimal Price { get; set; }

        [Display(Name = "summaryVisitManageViewModel_drugs", ResourceType = typeof(ResAdmin))]
        public string Drugs { get; set; }

        [Required(ErrorMessageResourceName = "summaryVisitManageViewModel_descriptionRequired", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "summaryVisitManageViewModel_description", ResourceType = typeof(ResAdmin))]
        public string Description { get; set; }
    }
}
