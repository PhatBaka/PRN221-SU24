using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Interfaces
{
    public interface IBaseDAO<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<DbSet<T>> GetAllAsync();
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        Task<object> AddAsync(T entity);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity, int id);
        Task<bool> DeleteAsync(T entity);
    }
}
