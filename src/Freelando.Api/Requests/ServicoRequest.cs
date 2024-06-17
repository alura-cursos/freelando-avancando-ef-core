using Freelando.Modelo;

namespace Freelando.Api.Requests;

public record ServicoRequest(Guid Id, string? Titulo, string? Descricao, StatusServico Status, ProjetoRequest Projeto);
