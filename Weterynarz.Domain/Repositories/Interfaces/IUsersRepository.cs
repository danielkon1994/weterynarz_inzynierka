using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Users;
using Weterynarz.Domain.ViewModels.Visit;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IQueryable<UserViewModel> GetListUsersViewModel();
        UsersManageViewModel GetEditViewModel(ApplicationUser account);
        Task<ApplicationUser> EditUser(UsersManageViewModel model);
        Task<bool> DeleteUser(string id);
        Task<bool> BanUser(string id);
        Task<bool> UnlockUser(string id);
        Task SavePassword(ApplicationUser user, string newPassword);
        Task<IEnumerable<SelectListItem>> GetVetsSelectList();
    }
}
