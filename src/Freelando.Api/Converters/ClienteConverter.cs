using Freelando.Api.Requests;
using Freelando.Api.Responses;
using Freelando.Modelo;

namespace Freelando.Api.Converters;

public class ClienteConverter
{
    private ProjetoConverter? _projetosConverter;

    public ClienteResponse EntityToResponse(Cliente? cliente)
    {
        _projetosConverter = new ProjetoConverter();

        if (cliente == null)
        {
            return new ClienteResponse(Guid.Empty, "", "", "", "");
        }

        return new ClienteResponse(cliente.Id, cliente.Nome, cliente.Cpf, cliente.Email, cliente.Telefone);
    }

    public Cliente RequestToEntity(ClienteRequest? cliente)
    {
        _projetosConverter = new ProjetoConverter();
        if (cliente == null) { return new Cliente(Guid.Empty, "", "", "", "", new List<Projeto>()); }
        return new Cliente(cliente.Id, cliente.Nome!, cliente.Cpf!, cliente.Email!, cliente.Telefone!, _projetosConverter.RequestListToEntityList(cliente.Projetos!));
    }

    public ICollection<ClienteResponse>? EntityListToResponseList(IEnumerable<Cliente>? clientes)
    {
        return (clientes is null)
            ? new List<ClienteResponse>()
            : clientes.Select(a => EntityToResponse(a)).ToList();
    }

    public ICollection<Cliente> RequestListToEntityList(IEnumerable<ClienteRequest>? clientes)
    {
        if (clientes is null) { return new List<Cliente>(); }
        return clientes.Select(a => RequestToEntity(a)).ToList();
    }
}