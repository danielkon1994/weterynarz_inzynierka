using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext _db;

        public AccountsRepository(ApplicationDbContext db)
        {
            _db = db;
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
    }
}
