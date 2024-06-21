using Freelando.Dados.Repository;

namespace Freelando.Dados.UnitOfWork;
public interface IUnitOfWork
{
    IEspecialidadeRepository EspecialidadeRepository { get; }
    Task Commit();
}

