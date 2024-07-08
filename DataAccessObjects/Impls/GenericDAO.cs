using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class GenericDAO<TEntity> : IGenericDAO<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        private static GenericDAO<TEntity> instance = null;
        private static readonly object InstanceLock = new object();

        public GenericDAO(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public static GenericDAO<TEntity> Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        AppDBContext context = new AppDBContext();
                        instance = new GenericDAO<TEntity>(context);
                    }
                    return instance;
                }
            }
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while fetching by Id", ex);
            }
        }

        public async Task<DbSet<TEntity>> GetAllAsync()
        {
            try
            {
                return _dbSet;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while fetching all entities", ex);
            }
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while fetching first entity with predicate", ex);
            }
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while fetching entities with predicate", ex);
            }
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while adding entity", ex);
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while updating entity", ex);
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while deleting entity", ex);
            }
        }
    }
}