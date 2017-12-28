using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Services.ViewModels.Accounts
{
    public class AccountsListViewModel : BaseViewModel<string>
    {
        public AccountsListViewModel()
        {
            Roles = new List<string>();
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public IList<string> Roles { get; set; }

        public string DisplayRoles {
            get
            {
                if(Roles.Count > 0)
                {
                    return string.Join(",", Roles);
                }
                return string.Empty;
            } 
        }
    }
}
