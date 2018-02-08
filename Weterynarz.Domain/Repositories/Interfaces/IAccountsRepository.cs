using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Accounts;
using Weterynarz.Domain.ViewModels.Visit;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        IQueryable<ApplicationUser> GetAllActive();
        IQueryable<ApplicationUser> GetAllNotDeleted();
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        Task<ApplicationUser> GetByIdFromUserManager(string id);
        ApplicationUser GetByIdNotDeleted(string id);
        void DeleteUser(ApplicationUser user);
        Task SaveChangesAsync();
        IQueryable<AccountViewModel> GetListUsersViewModel();
        AccountsManageViewModel GetEditViewModel(ApplicationUser account);
        Task<ApplicationUser> EditUser(AccountsManageViewModel model);
        Task<bool> DeleteUser(string id);
        Task<bool> BanUser(string id);
        Task<bool> UnlockUser(string id);
        Task SavePassword(ApplicationUser user, string newPassword);
        Task<IEnumerable<SelectListItem>> GetVetsSelectList();
        Task InsertAcync(ApplicationUser user);
        Task<ApplicationUser> InsertFromVisitFormAsync(VisitMakeVisitViewModel model);
        IEnumerable<SelectListItem> GetUsersSelectList();
        Task<IEnumerable<SelectListItem>> GetOwnersSelectList();
    }
}
