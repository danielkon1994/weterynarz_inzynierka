using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.AnimalTypes
{
    public class AnimalTypesManageViewModel : BaseViewModel<int>
    {
        [Display(Name = "manageViewModel_name", ResourceType = typeof(ResAdmin))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "manageViewModel_description", ResourceType = typeof(ResAdmin))]
        public string Description { get; set; }
    }
}
