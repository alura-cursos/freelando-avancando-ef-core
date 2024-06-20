using Freelando.Dados.Base;
using Freelando.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Dados.Repository;
public class EspecialidadeRepository : Repository<Especialidade>, IEspecialidadeRepository
{
    public EspecialidadeRepository(FreelandoContext context) : base(context)
    {
    }
}

