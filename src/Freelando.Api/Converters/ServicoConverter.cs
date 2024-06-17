using Freelando.Api.Requests;
using Freelando.Api.Responses;
using Freelando.Modelo;

namespace Freelando.Api.Converters;

public class ServicoConverter
{
    private ProjetoConverter? _projetoConverter;
    private CandidaturaConverter? _candidaturaConverter;

    public ServicoResponse EntityToResponse(Servico? servico)
    {
        _projetoConverter = new ProjetoConverter();


        if (servico == null)
        {
            return new ServicoResponse(Guid.Empty, null, null, StatusServico.Disponivel.ToString(), Guid.Empty);
        }

        ContratoResponse? contratoResponse = null;
        if (servico.Contrato != null)
        {
            ContratoConverter contratoConverter = new ContratoConverter();
            contratoResponse = contratoConverter.EntityToResponse(servico.Contrato);
        }

        return new ServicoResponse(servico.Id, servico.Titulo, servico.Descricao, servico.Status.ToString(), servico.ProjetoId);
    }

    public Servico RequestToEntity(ServicoRequest? servicoRequest)
    {
        _candidaturaConverter = new CandidaturaConverter();

        if (servicoRequest == null)
        {
            return new Servico(Guid.Empty, null, null, StatusServico.Disponivel, null, null, null);
        }

        return new Servico(servicoRequest.Id, servicoRequest.Titulo, servicoRequest.Descricao, servicoRequest.Status, null, _projetoConverter.RequestToEntity(servicoRequest.Projeto), null);
    }

    public ICollection<ServicoResponse> EntityListToResponseList(IEnumerable<Servico> servicos)
    {
        return (servicos == null)
            ? new List<ServicoResponse>()
            : servicos.Select(a => EntityToResponse(a)).ToList();
    }

    public ICollection<Servico> RequestListToEntityList(IEnumerable<ServicoRequest> servicosRequests)
    {
        if (servicosRequests == null)
        {
            return new List<Servico>();
        }

        return servicosRequests.Select(a => RequestToEntity(a)).ToList();
    }
}