using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class GenericDAO<TEntity> : IGenericDAO<TEntity> where TEntity : class
    {
        private readonly AppDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericDAO(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            try
            {
                await Task.Run(() => _dbSet.AddAsync(entity));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public async Task<TEntity> FindAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.SingleOrDefault(predicate));
        }

        public async Task<IQueryable<TEntity>> FindAll(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Where(predicate).AsQueryable());
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.AsQueryable());
        }

        public async Task DeleteRangeAsync(IQueryable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<bool> HardDeleteAsync(object key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HardDeleteIdAsync(object key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertRangeAsync(IQueryable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateByIdAsync(TEntity entity, object id)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity, object id)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(IQueryable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> AnyAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Any(predicate) ? _dbSet.First(predicate) : null);
        }

        public async Task<int> CountAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Count(predicate));
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<TEntity> FistOrDefault(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.FirstOrDefault(predicate));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> SaveChagesAysnc()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsMinAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Min(predicate));
        }

        public async Task<bool> IsMaxAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Max(predicate));
        }

        public async Task<TEntity> GetMinAsync()
        {
            return await _dbSet.MinAsync();
        }

        public async Task<TEntity> GetMaxAsync()
        {
            return await _dbSet.MaxAsync();
        }

        public async Task<bool> IsMaxAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.MaxAsync(predicate);
        }

        public async Task<bool> IsMinAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.MinAsync(predicate);
        }

        public async Task<bool> GetMinAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Min(predicate));
        }

        public async Task<bool> GetMaxAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Max(predicate));
        }

        public void Detach(TEntity entity)
        {
			_context.Entry(entity).State = EntityState.Detached;
		}

		public async Task<bool> UpdateAsync<TEntity>(TEntity entity)
		{
			return await Task.Run(async () =>
            {
                _context.Entry(entity).State = EntityState.Modified;
				return await _context.SaveChangesAsync() > 0;
			});
		}

        public async Task UpdateEntityAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
	}
}
