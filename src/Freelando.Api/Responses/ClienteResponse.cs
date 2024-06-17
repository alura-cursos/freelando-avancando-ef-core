namespace Freelando.Api.Responses;

public record ClienteResponse(Guid Id, string? Nome, string? Cpf, string? Email, string? Telefone);
