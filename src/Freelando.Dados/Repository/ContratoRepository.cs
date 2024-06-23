using Freelando.Dados.Repository.Base;
using Freelando.Modelo;

namespace Freelando.Dados.Repository;
public class ContratoRepository : Repository<Contrato>, IContratoRepository
{
    public ContratoRepository(FreelandoContext context) : base(context)
    {
    }
}
