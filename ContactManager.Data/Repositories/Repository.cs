using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data.Context;
using ContactManager.Data.Interfaces;
using ContactManager.Data.Entities;

namespace TodoListApp.Data.Repositories;
public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext context;
    protected readonly DbSet<T> dbSet;

    protected Repository(AppDbContext context)
    {
        this.context = context;
        this.dbSet = this.context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await this.dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await this.dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        _ = await this.dbSet.AddAsync(entity);
        _ = await this.context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _ = this.dbSet.Update(entity);
        _ = await this.context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await this.dbSet.FindAsync(id);
        if (entity != null)
        {
            _ = this.dbSet.Remove(entity);
            _ = await this.context.SaveChangesAsync();
        }
    }

    public async Task<PagedResult<T>> GetPagedAsync(PagingOptions options, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = this.dbSet.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(options.Skip)
            .Take(options.PageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = options.PageNumber,
            PageSize = options.PageSize
        };
    }
}