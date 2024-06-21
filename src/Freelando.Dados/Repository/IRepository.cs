using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Dados.Repository;
public interface IRepository<T>
{
    Task<IQueryable<T>> BuscarTodos();
    Task<T> BuscarPorId(Expression<Func<T, bool>> predicate);
    Task Adicionar(T entity);
    Task Atualizar(T entity);
    Task Deletar(T entity);
}

