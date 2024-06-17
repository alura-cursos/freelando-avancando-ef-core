using Freelando.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Modelo;
public class Profissional
{
    public Profissional()
    {
    }

    public Profissional(Guid id, string? nome, string? cpf, string? email, string? telefone, ICollection<Contrato> contratos, ICollection<Especialidade> especialidades)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        Contratos = contratos;
        Especialidades = especialidades;
    }
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public ICollection<Contrato> Contratos { get; set; }
    public ICollection<Especialidade> Especialidades { get; set; }
    public ICollection<ProfissionalEspecialidade> ProfissionaisEspecialidade { get; } = [];

}
