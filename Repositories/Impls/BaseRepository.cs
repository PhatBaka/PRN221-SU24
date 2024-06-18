using DataAccessObjects.Impls;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public async Task<T> GetByIdAsync(object id)
        {
            return await BaseDAO<T>.Instance.GetByIdAsync(id);
        }

        public async Task<object> AddAsync(T entity)
        {
            return await BaseDAO<T>.Instance.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await BaseDAO<T>.Instance.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await BaseDAO<T>.Instance.DeleteAsync(entity);
        }

        public async Task<DbSet<T>> GetAllAsync()
        {
            return await BaseDAO<T>.Instance.GetAllAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await BaseDAO<T>.Instance.GetFirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await BaseDAO<T>.Instance.GetWhereAsync(predicate);
        }
    }
}
