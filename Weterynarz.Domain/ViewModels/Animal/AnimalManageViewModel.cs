using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Animal
{
    public class AnimalManageViewModel : BaseViewModel<int>
    {
        public AnimalManageViewModel()
        {
            DiseaseIds = new Collection<int>();
        }

        [Required(ErrorMessage = "Imię zwierzęcia jest wymagane")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data urodzenia zwierzęcia jest wymagana")]
        [Display(Name = "Data urodzenia/przyjęcia")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Dodatkowe informacje")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Właściciel jest wymagany")]
        [Display(Name = "Właściciel")]
        public string OwnerId { get; set; }
        public IEnumerable<SelectListItem> OwnersSelectList { get; set; }

        [Required(ErrorMessage = "Typ zwierzęcia jest wymagany")]
        [Display(Name = "Typ")]
        public int AnimalTypeId { get; set; }
        public IEnumerable<SelectListItem> AnimalTypesSelectList { get; set; }

        [Display(Name = "Przebyte choroby")]
        public ICollection<int> DiseaseIds { get; set; }
        public IEnumerable<SelectListItem> DiseasesSelectList { get; set; }
    }
}
