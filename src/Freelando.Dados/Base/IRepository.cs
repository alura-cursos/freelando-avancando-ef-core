using System.Linq.Expressions;

namespace Freelando.Dados.Base;
public interface IRepository<T>
{
    Task<IQueryable<T>> Get();
    Task<T> GetById(Expression<Func<T, bool>> predicate);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}

