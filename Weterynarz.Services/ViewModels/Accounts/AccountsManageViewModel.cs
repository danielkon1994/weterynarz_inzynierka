using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Services.ViewModels.Accounts
{
    public class AccountsManageViewModel : BaseViewModel<string>
    {
        [Required]
        [Display(Name = "accountsManageViewModel_userName", ResourceType = typeof(ResAdmin))]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "accountsManageViewModel_email", ResourceType = typeof(ResAdmin))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "accountsManageViewModel_password", ResourceType = typeof(ResAdmin))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "accountsManageViewModel_confirmPassword", ResourceType = typeof(ResAdmin))]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "accountsManageViewModel_name", ResourceType = typeof(ResAdmin))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "accountsManageViewModel_surname", ResourceType = typeof(ResAdmin))]
        public string Surname { get; set; }

        [Display(Name = "accountsManageViewModel_address", ResourceType = typeof(ResAdmin))]
        public string Address { get; set; }

        [Display(Name = "accountsManageViewModel_houseNumber", ResourceType = typeof(ResAdmin))]
        public string HouseNumber { get; set; }

        [Display(Name = "accountsManageViewModel_city", ResourceType = typeof(ResAdmin))]
        public string City { get; set; }

        [Display(Name = "accountsManageViewModel_zipCode", ResourceType = typeof(ResAdmin))]
        public string ZipCode { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
