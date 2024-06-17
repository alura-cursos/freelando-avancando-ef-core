using Freelando.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Modelos;
public class ProfissionalEspecialidade
{
    public Guid EspecialidadeId { get; set; }
    public Especialidade Especialidade { get; set; }

    public Guid ProfissionalId { get; set; }
    public Profissional Profissional { get; set; }
}
