namespace Freelando.Modelo;
public class Servico
{
    public Servico()
    {
    }

    public Servico(Guid id, string? titulo, string? descricao, StatusServico status, Contrato contrato, Projeto projeto, ICollection<Candidatura> candidaturas)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Status = status;
        Contrato = contrato;
        Projeto = projeto;
        Candidaturas = candidaturas;
    }
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public StatusServico Status { get; set; }
    public Contrato Contrato { get; set; }
    public Guid ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
    public ICollection<Candidatura> Candidaturas { get; set; }

}
