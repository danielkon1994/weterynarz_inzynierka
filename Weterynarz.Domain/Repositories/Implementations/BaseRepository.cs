using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _db;
        
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public virtual async Task SoftDeleteAsync(T item)
        {
            item.Deleted = true;
            await this.SaveChangesAsync();
        }

        public virtual void Delete(T item)
        {
            _db.Set<T>().Remove(item);
        }

        public virtual IQueryable<T> GetAllActive()
        {
            return this.WhereActive().OrderBy(i => i.Id);
        }

        public virtual IQueryable<T> GetAllNotDeleted()
        {
            return this.WhereNotDeleted().OrderBy(i => i.Id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsNoTracking().OrderBy(i => i.Id);
        }

        public virtual T GetById(int id)
        {
            return _db.Set<T>().Where(i => !i.Deleted).FirstOrDefault(i => i.Id == id);
        }

        public virtual async Task<int> InsertAsync(T item)
        {
            await _db.Set<T>().AddAsync(item);
            await this.SaveChangesAsync();

            return item.Id;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> func)
        {
            return _db.Set<T>().Where(i => !i.Deleted).Where(func);
        }

        private IQueryable<T> WhereActive()
        {
            return _db.Set<T>().AsNoTracking().Where(i => i.Active && !i.Deleted);
        }

        private IQueryable<T> WhereNotDeleted()
        {
            return _db.Set<T>().AsNoTracking().Where(i => !i.Deleted);
        }
    }
}
