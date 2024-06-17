using Freelando.Modelo;

namespace Freelando.Api.Responses;

public record CandidaturaResponse(Guid Id, double? ValorProposto, string? DescricaoProposta, string? DuracaoProposta, string? Status, Guid ServicoId);
