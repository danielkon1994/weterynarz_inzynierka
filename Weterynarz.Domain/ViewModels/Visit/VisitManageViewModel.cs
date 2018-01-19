using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Visit
{
    public class VisitManageViewModel : BaseViewModel<int>
    {
        [Required(ErrorMessage = "Data wizyty jest wymagana")]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Dodatkowy opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Zwierzę jest wymagane")]
        [Display(Name = "Zwierze")]
        public int AnimalId { get; set; }
        public IEnumerable<SelectListItem> AnimalsSelectList { get; set; }

        [Required(ErrorMessage = "Doktor jest wymagany")]
        [Display(Name = "Doktor")]
        public string DoctorId { get; set; }
        public IEnumerable<SelectListItem> DoctorsSelectList { get; set; }
    }
}
