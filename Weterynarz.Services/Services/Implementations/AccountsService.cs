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

        public AccountsManageViewModel GetEditViewModel(ApplicationUser account)
        {
            AccountsManageViewModel model = new AccountsManageViewModel
            {
                Email = account.Email,
                Active = account.Active,
                Address = account.Address,
                City = account.City,
                UserName = account.UserName,
                HouseNumber = account.HouseNumber,
                Id = account.Id,
                Name = account.Name,
                Surname = account.Surname,
                ZipCode = account.ZipCode
            };

            return model;
        }

        public async Task<ApplicationUser> EditUser(AccountsManageViewModel model)
        {
            var user = _accountsRepository.GetById(model.Id);
            if(user != null)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.ZipCode = model.ZipCode;
                user.ModificationDate = DateTime.Now;
                user.HouseNumber = model.HouseNumber;
                user.Email = model.Email;
                user.City = model.City;
                user.Address = model.Address;

                await _accountsRepository.SaveChangesAsync();

                return user;
            }

            return null;
        }

        public async Task DeleteUser(string id)
        {
            var user = _accountsRepository.GetById(id);
            if(user != null)
            {
                _accountsRepository.DeleteUser(user);
                await _accountsRepository.SaveChangesAsync();
            }
        }
    }
}
