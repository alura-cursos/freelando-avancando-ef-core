using Freelando.Modelo;

namespace Freelando.Api.Responses;

public record ServicoResponse(Guid Id, string? Titulo, string? Descricao, string? Status, Guid ProjetoId);
