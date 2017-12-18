using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Services.ViewModels.Accounts
{
    public class AccountsListViewModel : BaseViewModel<string>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}
