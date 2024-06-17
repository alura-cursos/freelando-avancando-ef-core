using Freelando.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Modelos;
public class ProjetoEspecialidade
{
    public Guid EspecialidadeId { get; set; }
    public Especialidade Especialidade { get; set; }

    public Guid ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
}
