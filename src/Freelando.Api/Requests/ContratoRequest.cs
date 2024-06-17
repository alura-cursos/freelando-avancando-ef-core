using Freelando.Modelo;
using Freelando.Modelos;

namespace Freelando.Api.Requests;

public record ContratoRequest(Guid Id, double Valor, Vigencia? Vigencia, ServicoRequest Servico, ProfissionalRequest profissional);
