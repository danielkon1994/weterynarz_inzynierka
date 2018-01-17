﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;
using Weterynarz.Basic.Const;
using Microsoft.EntityFrameworkCore;
using Weterynarz.Domain.ViewModels.Users;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.ViewModels.Visit;
using CryptoHelper;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        public IAccountsRepository _accountsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersRepository(IAccountsRepository accountsRepository, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._accountsRepository = accountsRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<UserViewModel> GetListUsersViewModel()
        {
            return _accountsRepository.GetAllNotDeleted().Select(a => new UserViewModel
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
                UserName = a.UserName,
                Roles = a.Roles.Select(i => i.RoleId).ToList()
            });
        }

        public UsersManageViewModel GetEditViewModel(ApplicationUser account)
        {
            UsersManageViewModel model = new UsersManageViewModel
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

        public async Task<ApplicationUser> EditUser(UsersManageViewModel model)
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

        public async Task<bool> DeleteUser(string id)
        {
            var user = _accountsRepository.GetById(id);
            if(user != null)
            {
                _accountsRepository.DeleteUser(user);
                await _accountsRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> BanUser(string id)
        {
            var user = _accountsRepository.GetById(id);
            if (user != null)
            {
                user.Active = false;

                await _accountsRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UnlockUser(string id)
        {
            var user = _accountsRepository.GetByIdNotDeleted(id);
            if (user != null)
            {
                user.Active = true;

                await _accountsRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task SavePassword(ApplicationUser user, string newPassword)
        {
            user.PasswordHash = newPassword;

            await _accountsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetVetsSelectList()
        {
            List<SelectListItem> vetsList = new List<SelectListItem>();
            vetsList.Add(new SelectListItem { Value = "", Text = "-- wybierz --", Disabled = true, Selected = true });

            var users = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);            
            if(users != null)
            {
                foreach(var user in users)
                {
                    vetsList.Add(new SelectListItem {
                        Text = $"{user.Name} {user.Surname}",
                        Value = user.Id
                    });
                }
            }
            return vetsList.AsEnumerable();
        }
    }
}