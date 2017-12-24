﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Services.ViewModels.Accounts;
using Weterynarz.Services.ViewModels.AnimalTypes;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface IAccountsService
    {
        IQueryable<AccountsListViewModel> GetListUsersViewModel();
        AccountsManageViewModel GetEditViewModel(ApplicationUser account);
        Task<ApplicationUser> EditUser(AccountsManageViewModel model);
        Task DeleteUser(string id);
    }
}
