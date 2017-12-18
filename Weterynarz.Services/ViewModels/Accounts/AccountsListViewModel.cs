using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Services.ViewModels.Accounts
{
    public class AccountsListViewModel : BaseViewModel<string>
    {
        [Required]
        [Display(Name = "accountsListViewModel_name", ResourceType = typeof(ResAdmin))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "accountsListViewModel_surname", ResourceType = typeof(ResAdmin))]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "accountsListViewModel_userName", ResourceType = typeof(ResAdmin))]
        public string UserName { get; set; }

        [Display(Name = "accountsListViewModel_address", ResourceType = typeof(ResAdmin))]
        public string Address { get; set; }

        [Display(Name = "accountsListViewModel_houseNumber", ResourceType = typeof(ResAdmin))]
        public string HouseNumber { get; set; }

        [Display(Name = "accountsListViewModel_city", ResourceType = typeof(ResAdmin))]
        public string City { get; set; }

        [Display(Name = "accountsListViewModel_zipCode", ResourceType = typeof(ResAdmin))]
        public string ZipCode { get; set; }


    }
}
