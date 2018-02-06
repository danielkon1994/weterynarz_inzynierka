using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.Users
{
    public class UsersRegisterViewModel : BaseViewModel<string>
    {
        [Required(ErrorMessage = "Pole Nazwa użytkownika jest wymagane")]
        [Display(Name = "usersManageViewModel_userName", ResourceType = typeof(ResAdmin))]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole Adres e-mail jest wymagane")]
        [EmailAddress]
        [Display(Name = "usersManageViewModel_email", ResourceType = typeof(ResAdmin))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "usersManageViewModel_password", ResourceType = typeof(ResAdmin))]
        [Required(ErrorMessage = "Pole Hasło jest wymagane")]
        public string Password { get; set; }

        [Display(Name = "usersManageViewModel_confirmPassword", ResourceType = typeof(ResAdmin))]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pole Imię jest wymagane")]
        [Display(Name = "usersManageViewModel_name", ResourceType = typeof(ResAdmin))]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole Nazwisko wymagane")]
        [Display(Name = "usersManageViewModel_surname", ResourceType = typeof(ResAdmin))]
        public string Surname { get; set; }

        [Display(Name = "usersManageViewModel_address", ResourceType = typeof(ResAdmin))]
        public string Address { get; set; }

        [Display(Name = "usersManageViewModel_houseNumber", ResourceType = typeof(ResAdmin))]
        public string HouseNumber { get; set; }

        [Display(Name = "usersManageViewModel_city", ResourceType = typeof(ResAdmin))]
        public string City { get; set; }

        [Display(Name = "usersManageViewModel_zipCode", ResourceType = typeof(ResAdmin))]
        public string ZipCode { get; set; }
    }
}
