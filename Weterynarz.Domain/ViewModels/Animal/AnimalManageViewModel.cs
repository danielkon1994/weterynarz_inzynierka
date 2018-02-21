using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.Animal
{
    public class AnimalManageViewModel : BaseViewModel<int>
    {
        public AnimalManageViewModel()
        {
            DiseaseIds = new Collection<int>();
        }

        [Required(ErrorMessageResourceName = "animalManageViewModel_nameRequired", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "animalManageViewModel_name", ResourceType = typeof(ResAdmin))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "animalManageViewModel_birthdayRequired", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "animalManageViewModel_birthday", ResourceType = typeof(ResAdmin))]
        public DateTime BirthDay { get; set; }

        [Display(Name = "animalManageViewModel_description", ResourceType = typeof(ResAdmin))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "animalManageViewModel_ownerRequired", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "animalManageViewModel_owner", ResourceType = typeof(ResAdmin))]
        public string OwnerId { get; set; }
        public IEnumerable<SelectListItem> OwnersSelectList { get; set; }

        [Required(ErrorMessageResourceName = "animalManageViewModel_animalTypeRequired", ErrorMessageResourceType = typeof(ResAdmin))]
        [Display(Name = "animalManageViewModel_animalType", ResourceType = typeof(ResAdmin))]
        public int AnimalTypeId { get; set; }
        public IEnumerable<SelectListItem> AnimalTypesSelectList { get; set; }

        [Display(Name = "animalManageViewModel_diseases", ResourceType = typeof(ResAdmin))]
        public ICollection<int> DiseaseIds { get; set; }
        public IEnumerable<SelectListItem> DiseasesSelectList { get; set; }

        [Display(Name = "animalManageViewModel_medicalExamination", ResourceType = typeof(ResAdmin))]
        public ICollection<int> MedicalExaminationIds { get; set; }
        public IEnumerable<SelectListItem> MedicalExaminationSelectList { get; set; }
    }
}
