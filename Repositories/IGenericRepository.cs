using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> FindAsync(Func<TEntity, bool> predicate);
        public Task<IQueryable<TEntity>> FindAll(Func<TEntity, bool> predicate);
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<IQueryable<TEntity>> GetAllAsync();
        public Task DeleteRangeAsync(IQueryable<TEntity> entities);
        public Task<TEntity> GetByIdAsync(object id);
        public Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<bool> HardDeleteAsync(object key);
        public Task<bool> DeleteAsync(TEntity entity);
        public Task<bool> HardDeleteIdAsync(object key);
        public Task<bool> InsertAsync(TEntity entity);
        public Task<bool> InsertRangeAsync(IQueryable<TEntity> entities);
        public Task<bool> UpdateByIdAsync(TEntity entity, object id);
        public Task<bool> UpdateAsync(TEntity entity);
        public Task<bool> UpdateRangeAsync(IQueryable<TEntity> entities);
        public Task<TEntity> AnyAsync(Func<TEntity, bool> predicate);
        public Task<int> CountAsync(Func<TEntity, bool> predicate);
        public Task<int> CountAsync();
        public Task<TEntity> FistOrDefault(Func<TEntity, bool> predicate);
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<bool> SaveChagesAysnc();
        public Task<bool> IsMinAsync(Func<TEntity, bool> predicate);
        public Task<bool> IsMaxAsync(Func<TEntity, bool> predicate);
        public Task<TEntity> GetMinAsync();
        public Task<TEntity> GetMaxAsync();
        public Task<bool> IsMaxAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<bool> IsMinAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<bool> GetMinAsync(Func<TEntity, bool> predicate);
        public Task<bool> GetMaxAsync(Func<TEntity, bool> predicate);
		public void Detach(TEntity entity);
        public Task<TEntity> InsertEntityAsync(TEntity entity);
        public Task UpdateEntityAsync(TEntity entity);
	}
}
