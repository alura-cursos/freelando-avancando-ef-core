namespace Freelando.Api.Requests;

public record ProfissionalRequest(Guid Id, string? Nome, string? Cpf, string? Email, string? Telefone, ICollection<EspecialidadeRequest> Especialidades);

