using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        IQueryable<ApplicationUser> GetAllActive();
        IQueryable<ApplicationUser> GetAllNotDeleted();
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
    }
}
