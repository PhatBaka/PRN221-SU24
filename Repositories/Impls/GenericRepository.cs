using DataAccessObjects.Impls;
using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> AddAsync(TEntity entity) => GenericDAO<TEntity>.Instance.AddAsync(entity);

        public Task<bool> DeleteAsync(TEntity entity) => GenericDAO<TEntity>.Instance.DeleteAsync(entity);

        public Task<DbSet<TEntity>> GetAllAsync() => GenericDAO<TEntity>.Instance.GetAllAsync();

        public Task<TEntity> GetByIdAsync(object id) => GenericDAO<TEntity>.Instance.GetByIdAsync(id);

        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => GenericDAO<TEntity>.Instance.GetFirstOrDefaultAsync(predicate);

        public Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate) => GenericDAO<TEntity>.Instance.GetWhereAsync(predicate);

        public Task<bool> UpdateAsync(TEntity entity) => GenericDAO<TEntity>.Instance.UpdateAsync(entity);
    }
}