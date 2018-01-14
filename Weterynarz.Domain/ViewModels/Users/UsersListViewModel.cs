using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Weterynarz.Basic.Const;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.Users
{
    public class UsersListViewModel
    {

        public IQueryable<UserViewModel> ListUsers { get; set; }
    }
}
