using Freelando.Api.Requests;
using Freelando.Api.Responses;
using Freelando.Modelo;

namespace Freelando.Api.Converters;

public class CandidaturaConverter
{
    private ServicoConverter _servicoConverter;

    public CandidaturaResponse EntityToResponse(Candidatura? candidatura)
    {
        if (candidatura == null)
        {
            return new CandidaturaResponse(Guid.Empty, 0.0, "", DuracaoEmDias.DeQuinzeATrinta.ToString(), StatusCandidatura.Aprovada.ToString(), Guid.Empty);
        }

        return new CandidaturaResponse(candidatura.Id, candidatura.ValorProposto, candidatura.DescricaoProposta,candidatura.DuracaoProposta.ToString(), candidatura.Status.ToString(), candidatura.ServicoId);
    }

    public Candidatura RequestToEntity(CandidaturaRequest? candidaturaRequest)
    {
        _servicoConverter = new ServicoConverter();

        if (candidaturaRequest == null)
        {
            return new Candidatura(Guid.Empty, 0.0, null,  DuracaoEmDias.MenosDeUm, StatusCandidatura.Aprovada, null);
        }

        return new Candidatura(candidaturaRequest.Id, candidaturaRequest.ValorProposto, candidaturaRequest.DescricaoProposta, candidaturaRequest.DuracaoProposta!.Value, candidaturaRequest.Status!.Value, _servicoConverter.RequestToEntity(candidaturaRequest.Servico));
    }

    public ICollection<CandidaturaResponse> EntityListToResponseList(IEnumerable<Candidatura> candidaturas)
    {
        return (candidaturas == null)
            ? new List<CandidaturaResponse>()
            : candidaturas.Select(a => EntityToResponse(a)).ToList();
    }

    public ICollection<Candidatura> RequestListToEntityList(IEnumerable<CandidaturaRequest> candidaturaRequests)
    {
        if (candidaturaRequests == null)
        {
            return new List<Candidatura>();
        }

        return candidaturaRequests.Select(a => RequestToEntity(a)).ToList();
    }
}
