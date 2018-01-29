using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Const;

namespace Weterynarz.Domain.ViewModels.Visit
{
    public class VisitMakeVisitViewModel
    {
        #region Contact details    
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła muszą być takie same")]
        [Display(Name = "Powtórz hasło")]
        public string ComparePassword { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Numer domu / mieszkania")]
        public string HomeNumber { get; set; }

        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }
        #endregion

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> VetsSelectList { get; set; }
        [Display(Name = "Lekarz")]
        public string VetId { get; set; }

        public IEnumerable<SelectListItem> AnimalsSelectList { get; set; }
        [Display(Name = "Zwierzę")]
        public int? AnimalId { get; set; }

        public IEnumerable<SelectListItem> AnimalTypesSelectList { get; set; }
        [Display(Name = "Typ zwierzęcia")]
        public int AnimalTypeId { get; set; }

        [Display(Name = "Imię zwierzęcia")]
        public string AnimalName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia / przyjęcia zwierzęcia")]
        public DateTime AnimalBirthdate { get; set; }

        [Display(Name = "Powód wizyty")]
        public string ReasonVisit { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }
    }
}
