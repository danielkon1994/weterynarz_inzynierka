using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.Accounts;
using Weterynarz.Services.ViewModels.AnimalTypes;

namespace Weterynarz.Services.Services.Implementations
{
    public class AccountsService : IAccountsService
    {
        public IAccountsRepository _accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            this._accountsRepository = accountsRepository;
        }

        public IQueryable<AccountsListViewModel> GetListUsersViewModel()
        {
            return _accountsRepository.GetAllNotDeleted().Select(a => new AccountsListViewModel
            {
                Id = a.Id,
                Active = a.Active,
                Name = a.Name,
                Surname = a.Surname,
                Address = a.Address,
                City = a.City,
                CreationDate = a.CreationDate,
                HouseNumber = a.HouseNumber,
                ZipCode = a.ZipCode,
                UserName = a.UserName
            });
        }

        public AccountsManageViewModel GetEditViewModel(int id)
        {
            AccountsManageViewModel model;
            var account = _accountsRepository.GetById(id);
            if (account != null)
            {
                model = new AccountsManageViewModel
                {
                    Active = account.Active,
                    Name = account.Name,
                    Id = account.Id,
                    Surname = account.Surname,

                };

                return model;
            }

            return null;
        }
    }
}
