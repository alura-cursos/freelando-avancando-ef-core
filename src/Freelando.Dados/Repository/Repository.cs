using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Freelando.Dados.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    private FreelandoContext _context;
    public Repository(FreelandoContext context)
    {
        _context = context;
    }
    public virtual async Task Adicionar(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task Deletar(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IQueryable<T>> BuscarTodos()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public async Task<T> BuscarPorId(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public virtual async Task Atualizar(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

}
