using Freelando.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelando.Modelo;
public class Especialidade
{
    public Especialidade()
    {
    }
    public Especialidade(Guid id, string? descricao, ICollection<Projeto> projetos, ICollection<Profissional> profissionais)
    {
        Id = id;
        Descricao = descricao;
        Projetos = projetos;
        Profissionais = profissionais;
    }
    public Guid Id { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Projeto> Projetos { get; set; }
    public ICollection<ProjetoEspecialidade> ProjetosEspecialidade { get; } = [];
    public ICollection<Profissional> Profissionais { get; set; }
    public ICollection<ProfissionalEspecialidade> ProfissionaisEspecialidade { get; } = [];

}
