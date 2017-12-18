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
        [Display(Name = "accountsManageViewModel_name", ResourceType = typeof(ResAdmin))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "accountsManageViewModel_surname", ResourceType = typeof(ResAdmin))]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "accountsManageViewModel_userName", ResourceType = typeof(ResAdmin))]
        public string UserName { get; set; }

        [Display(Name = "accountsManageViewModel_address", ResourceType = typeof(ResAdmin))]
        public string Address { get; set; }

        [Display(Name = "accountsManageViewModel_houseNumber", ResourceType = typeof(ResAdmin))]
        public string HouseNumber { get; set; }

        [Display(Name = "accountsManageViewModel_city", ResourceType = typeof(ResAdmin))]
        public string City { get; set; }

        [Display(Name = "accountsManageViewModel_zipCode", ResourceType = typeof(ResAdmin))]
        public string ZipCode { get; set; }
    }
}
