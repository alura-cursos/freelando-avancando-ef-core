using Freelando.Modelo;
using Freelando.Modelos;

namespace Freelando.Api.Responses;

public record ContratoResponse(Guid Id, double? Valor, Vigencia? Vigencia, Guid ServicoId, Guid ProfissionalId);
