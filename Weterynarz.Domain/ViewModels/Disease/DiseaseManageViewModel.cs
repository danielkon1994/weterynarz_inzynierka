using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Disease
{
    public class DiseaseManageViewModel : BaseViewModel<int>
    {
        public DiseaseManageViewModel()
        {
        }

        [Required(ErrorMessage = "Nazwa choroby jest wymagana")]
        [Display(Name = "Nazwa choroby")]
        public string Name { get; set; }

        [Display(Name = "Dodatkowy opis choroby")]
        public string Description { get; set; }
    }
}
