namespace Freelando.Api.Requests;

public record ClienteRequest(Guid Id, string? Nome, string? Cpf, string? Email, string? Telefone, ICollection<ProjetoRequest> Projetos);
