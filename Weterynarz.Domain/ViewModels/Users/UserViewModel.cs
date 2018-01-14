using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Basic.Const;

namespace Weterynarz.Domain.ViewModels.Users
{
    public class UserViewModel : BaseViewModel<string>
    {
        public UserViewModel()
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

        public string DisplayRoles
        {
            get
            {
                if (Roles.Count > 0)
                {
                    List<string> rolesList = new List<string>();
                    foreach (string role in Roles)
                    {
                        rolesList.Add(UserRoles.TranslateRole(role));
                    }
                    return string.Join(", ", rolesList);
                }
                return string.Empty;
            }
        }
    }
}
