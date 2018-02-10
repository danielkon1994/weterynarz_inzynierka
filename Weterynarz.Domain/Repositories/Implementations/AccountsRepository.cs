using CryptoHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Accounts;
using Weterynarz.Domain.ViewModels.Visit;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _db.Users.AsQueryable();
        }

        public IQueryable<ApplicationUser> GetAllNotDeleted()
        {
            return this.GetAll().Where(a => !a.Deleted);
        }

        public IQueryable<ApplicationUser> GetAllActive()
        {
            return this.GetAll().Where(a => !a.Deleted && a.Active);
        }

        public ApplicationUser GetById(string id)
        {
            return this.GetAll().Where(a => !a.Deleted && a.Active).FirstOrDefault(i => i.Id == id);
        }

        public async Task<ApplicationUser> GetByIdFromUserManager(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public ApplicationUser GetByIdNotDeleted(string id)
        {
            return GetAllNotDeleted().FirstOrDefault(i => i.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void DeleteUser(ApplicationUser user)
        {
            _db.Users.Remove(user);
        }

        public IQueryable<AccountViewModel> GetListUsersViewModel()
        {
            return this.GetAllNotDeleted().Select(a => new AccountViewModel
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
            var user = this.GetById(model.Id);
            if (user != null)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.ZipCode = model.ZipCode;
                user.ModificationDate = DateTime.Now;
                user.HouseNumber = model.HouseNumber;
                user.Email = model.Email;
                user.City = model.City;
                user.Address = model.Address;

                await this.SaveChangesAsync();

                return user;
            }

            return null;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = this.GetById(id);
            if (user != null)
            {
                this.DeleteUser(user);
                await this.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> BanUser(string id)
        {
            var user = this.GetById(id);
            if (user != null)
            {
                user.Active = false;

                await this.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UnlockUser(string id)
        {
            var user = this.GetByIdNotDeleted(id);
            if (user != null)
            {
                user.Active = true;

                await this.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task SavePassword(ApplicationUser user, string newPassword)
        {
            user.PasswordHash = newPassword;

            await this.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetVetsSelectList()
        {
            List<SelectListItem> vetsList = new List<SelectListItem>();

            vetsList.Add(new SelectListItem { Disabled = true, Group = null, Selected = true, Text = "--- wybierz ---", Value = "" });

            var users = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
            if (users != null)
            {
                foreach (var user in users)
                {
                    vetsList.Add(new SelectListItem
                    {
                        Text = $"{user.Name} {user.Surname}",
                        Value = user.Id
                    });
                }
            }
            return vetsList.AsEnumerable();
        }

        public async Task<IEnumerable<SelectListItem>> GetOwnersSelectList()
        {
            List<SelectListItem> ownersList = new List<SelectListItem>();

            ownersList.Add(new SelectListItem { Disabled = true, Group = null, Selected = true, Text = "--- wybierz ---", Value = "" });

            var users = await _userManager.GetUsersInRoleAsync(UserRoles.Client);
            if (users != null)
            {
                foreach (var user in users)
                {
                    ownersList.Add(new SelectListItem
                    {
                        Text = $"{user.Name} {user.Surname}",
                        Value = user.Id
                    });
                }
            }
            return ownersList.AsEnumerable();
        }

        public async Task InsertAcync(ApplicationUser user)
        {
            _db.Users.Add(user);

            await _db.SaveChangesAsync();
        }

        public async Task<ApplicationUser> InsertFromVisitFormAsync(VisitMakeVisitViewModel model)
        {
            if(!string.IsNullOrEmpty(model.UserId))
            {
                return await _userManager.FindByIdAsync(model.UserId);
            }

            ApplicationUser client = new ApplicationUser()
            {
                Active = true,
                Address = model.Address,
                City = model.City,
                CreationDate = DateTime.Now,
                Deleted = false,
                Email = model.Email,
                EmailConfirmed = true,
                HouseNumber = model.HomeNumber,
                Name = model.Name,
                Surname = model.Surname,
                ZipCode = model.ZipCode,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(client, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(client, UserRoles.Client);
            }

            return client;
        }

        public IEnumerable<SelectListItem> GetUsersSelectList()
        {
            List<SelectListItem> usersList = new List<SelectListItem>();

            var users = _userManager.Users;
            if (users != null)
            {
                foreach (var user in users)
                {
                    usersList.Add(new SelectListItem
                    {
                        Text = $"{user.Name} {user.Surname}",
                        Value = user.Id
                    });
                }
            }
            return usersList.AsEnumerable();
        }
    }
}
