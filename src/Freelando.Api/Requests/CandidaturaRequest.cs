using Freelando.Api.Responses;
using Freelando.Modelo;

namespace Freelando.Api.Requests;

public record CandidaturaRequest(Guid Id, double ValorProposto, string? DescricaoProposta, DuracaoEmDias? DuracaoProposta, StatusCandidatura? Status, ServicoRequest Servico);
