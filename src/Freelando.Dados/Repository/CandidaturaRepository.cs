using Freelando.Dados.Repository.Base;
using Freelando.Dados.Repository.Interfaces;
using Freelando.Modelo;

namespace Freelando.Dados.Repository;
public class CandidaturaRepository : Repository<Candidatura>, ICandidaturaRepository
{
    public CandidaturaRepository(FreelandoContext context) : base(context)
    {
    }
}
