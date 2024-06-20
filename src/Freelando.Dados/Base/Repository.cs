using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Dados.Base;
public class Repository<T> : IRepository<T> where T : class
{
    private FreelandoContext _context;
    public Repository(FreelandoContext context)
    {
        _context = context;
    }

    public virtual async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);        
    }

    public virtual async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await this._context.SaveChangesAsync();
    }

    public virtual async Task<IQueryable<T>> Get()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public virtual async Task<T> GetById(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public virtual async Task<T?> FindByIdAsync(int id)
    {
        return await this._context.Set<T>().FindAsync(id);
    }

    public virtual async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

}

