using Freelando.Dados.Repository.Interfaces;

namespace Freelando.Dados.UnitOfWork;
public interface IUnitOfWork
{
    IEspecialidadeRepository EspecialidadeRepository { get; }
    Task Commit();
}

