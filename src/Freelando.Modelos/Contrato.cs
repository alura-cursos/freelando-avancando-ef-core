using Freelando.Modelo;
using Freelando.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Modelo;
public class Contrato
{
    public Contrato()
    {
    }

    public Contrato(Guid id, double valor, Vigencia? vigencia, Servico servico, Profissional profissional)
    {
        Id = id;
        Valor = valor;
        Vigencia = vigencia;
        Servico = servico;
        Profissional = profissional;
    }
    public Guid Id { get; set; }
    public double Valor { get; set; }
    public Vigencia? Vigencia { get; set; }
    public Guid ServicoId { get; set; }
    public Servico Servico { get; set; }
    public Guid ProfissionalId { get; set; }
    public Profissional Profissional { get; set; }

}
