using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Enum;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.PriceList
{
    public class PriceListManageViewModel : BaseViewModel<int>
    {
        public PriceListManageViewModel()
        {
        }

        [Required(ErrorMessageResourceName = "priceListManageViewMode_errorName", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "priceListManageViewMode_name", ResourceType = typeof(ResAdmin))]
        public string Name { get; set; }

        [Display(Name = "priceListManageViewMode_type", ResourceType = typeof(ResAdmin))]
        public PriceListEntryType Type { get; set; }

        [Required(ErrorMessageResourceName = "priceListManageViewMode_errorPrice", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "priceListManageViewMode_price", ResourceType = typeof(ResAdmin))]        
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceName = "priceListManageViewMode_errorExaminationId", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "priceListManageViewMode_medicalExamination", ResourceType = typeof(ResAdmin))]
        public int? SelectedMedicalExaminationId { get; set; }
        public IEnumerable<SelectListItem> MedicalExaminationSelectList { get; set; }
    }
}
