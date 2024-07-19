using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IGenericDAO<TEntity> dao;

        public GenericRepository(IGenericDAO<TEntity> dao)
        {
            this.dao = dao;
        }

        public async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            return await dao.InsertEntityAsync(entity);
        }

        public async Task<TEntity> AnyAsync(Func<TEntity, bool> predicate)
        {
            return await dao.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Func<TEntity, bool> predicate)
        {
            return await dao.CountAsync(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await dao.CountAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
			return await dao.DeleteAsync(entity);
        }

        public async Task DeleteRangeAsync(IQueryable<TEntity> entities)
        {
            await dao.DeleteRangeAsync(entities);
        }

		public void Detach(TEntity jewelryToUpdate)
		{
			dao.Detach(jewelryToUpdate);
		}

		public async Task<IQueryable<TEntity>> FindAll(Func<TEntity, bool> predicate)
        {
            return await dao.FindAll(predicate);
        }

        public async Task<TEntity> FindAsync(Func<TEntity, bool> predicate)
        {
            return await dao.FindAsync(predicate);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dao.FindAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dao.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FistOrDefault(Func<TEntity, bool> predicate)
        {
            return await dao.FistOrDefault(predicate);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await dao.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await dao.GetByIdAsync(id);
        }

        public async Task<TEntity> GetMaxAsync()
        {
            return await dao.GetMaxAsync();
        }

        public async Task<bool> GetMaxAsync(Func<TEntity, bool> predicate)
        {
            return await dao.GetMaxAsync(predicate);
        }

        public async Task<TEntity> GetMinAsync()
        {
            return await dao.GetMinAsync();
        }

        public async Task<bool> GetMinAsync(Func<TEntity, bool> predicate)
        {
            return await dao.GetMinAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dao.GetWhereAsync(predicate);
        }

        public async Task<bool> HardDeleteAsync(object key)
        {
            return await dao.HardDeleteAsync(key);
        }

        public async Task<bool> HardDeleteIdAsync(object key)
        {
            return await dao.HardDeleteIdAsync(key);
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            return await dao.InsertAsync(entity);
        }

        public async Task<bool> InsertRangeAsync(IQueryable<TEntity> entities)
        {
            return await dao.InsertRangeAsync(entities);
        }

        public async Task<bool> IsMaxAsync(Func<TEntity, bool> predicate)
        {
            return await dao.IsMaxAsync(predicate);
        }

        public async Task<bool> IsMaxAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dao.IsMaxAsync(predicate);
        }

        public async Task<bool> IsMinAsync(Func<TEntity, bool> predicate)
        {
            return await dao.IsMinAsync(predicate);
        }

        public async Task<bool> IsMinAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dao.IsMinAsync(predicate);
        }

        public async Task<bool> SaveChagesAysnc()
        {
            return await dao.SaveChagesAysnc();
        }

		public async Task<bool> UpdateAsync(TEntity entity)
		{
			return await dao.UpdateAsync(entity);
		}

		public async Task<bool> UpdateByIdAsync(TEntity entity, object id)
        {
            return await dao.UpdateByIdAsync(entity, id);
        }

        public async Task<bool> UpdateRangeAsync(IQueryable<TEntity> entities)
        {
            return await dao.UpdateRangeAsync(entities);
        }

        public async Task UpdateEntityAsync(TEntity entity)
        {
            await dao.UpdateEntityAsync(entity);
        }
    }
}
