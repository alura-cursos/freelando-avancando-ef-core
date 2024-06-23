using Freelando.Dados.Repository.Base;
using Freelando.Dados.Repository.Interfaces;
using Freelando.Modelo;

namespace Freelando.Dados.Repository;
public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(FreelandoContext context) : base(context)
    {
    }
}
