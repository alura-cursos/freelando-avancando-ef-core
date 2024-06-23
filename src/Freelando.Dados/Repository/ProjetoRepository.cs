using Freelando.Dados.Repository.Base;
using Freelando.Modelo;

namespace Freelando.Dados.Repository;
public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
{
    public ProjetoRepository(FreelandoContext context) : base(context)
    {
    }
}
