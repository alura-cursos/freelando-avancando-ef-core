using Freelando.Dados.Repository;
using Freelando.Modelo;

namespace Freelando.Dados;
public class EspecialidadeRepository : Repository<Especialidade>, IEspecialidadeRepository
{
    public EspecialidadeRepository(FreelandoContext context) : base(context)
    {
    }
}
