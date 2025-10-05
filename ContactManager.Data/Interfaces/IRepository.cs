using ContactManager.Data.Entities;
using System.Linq.Expressions;

namespace ContactManager.Data.Interfaces;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<PagedResult<T>> GetPagedAsync(PagingOptions options, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
}