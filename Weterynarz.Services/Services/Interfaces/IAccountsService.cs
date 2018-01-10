﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        IQueryable<AccountViewModel> GetListUsersViewModel();
        AccountsManageViewModel GetEditViewModel(ApplicationUser account);
        Task<ApplicationUser> EditUser(AccountsManageViewModel model);
        Task<bool> DeleteUser(string id);
        Task<bool> BanUser(string id);
        Task<bool> UnlockUser(string id);
        Task SavePassword(ApplicationUser user, string newPassword);
        Task<IEnumerable<SelectListItem>> GetVetsSelectList();
    }
}
