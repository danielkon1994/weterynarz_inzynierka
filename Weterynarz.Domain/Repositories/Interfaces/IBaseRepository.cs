using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IQueryable<T> GetAllActive();
        IQueryable<T> GetAllNotDeleted();
        IQueryable<T> GetAll();
        Task<int> InsertAsync(T item);
        Task SoftDeleteAsync(T item);
        void Delete(T item);
        Task SaveChangesAsync();
    }
}
