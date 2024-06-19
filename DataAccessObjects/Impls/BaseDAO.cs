using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class BaseDAO<T> : IBaseDAO<T> where T : class
    {
        private static BaseDAO<T> instance = null;
        private static readonly object InstanceLock = new object();

        private readonly AppDBContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseDAO(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public static BaseDAO<T> Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        AppDBContext context = new AppDBContext();
                        instance = new BaseDAO<T>(context);
                    }
                    return instance;
                }
            }
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<DbSet<T>> GetAllAsync()
        {
            return _dbSet;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<object> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            var entityEntry = _context.Entry(entity);
            var idProperty = entityEntry.Metadata.FindPrimaryKey().Properties[0];

            return entityEntry.Property(idProperty.Name).CurrentValue;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T entity, int id)
        {
            var existedEntity = await GetByIdAsync(id);
            if (existedEntity == entity)
            {
                return true;
            }
            _context.Entry(existedEntity).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
