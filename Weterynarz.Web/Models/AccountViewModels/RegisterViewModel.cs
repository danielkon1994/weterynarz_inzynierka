using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weterynarz.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Numer domu/budynku")]
        public string HouseNumber { get; set; }

        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }

        public IEnumerable<string> RolesList { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
