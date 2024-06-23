using Freelando.Dados.Repository.Base;
using Freelando.Modelo;

namespace Freelando.Dados.Repository;
public class ProfissionalRepository : Repository<Profissional>, IProfissionalRepository
{
    public ProfissionalRepository(FreelandoContext context) : base(context)
    {
    }
}
