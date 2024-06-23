using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Freelando.Api.Endpoints;

public static class CandidaturaExtension
{
    public static void AddEndPointCandidaturas(this WebApplication app)
    {
        app.MapGet("/candidaturas", async ([FromServices] CandidaturaConverter converter, [FromServices] IUnitOfWork unitOfWork) =>
        {
            var candidatura = converter.EntityListToResponseList(await unitOfWork.CandidaturaRepository.BuscarTodos());

            return Results.Ok(await Task.FromResult(candidatura));
        }).WithTags("Candidatura").WithOpenApi();

        app.MapPost("/candidatura", async ([FromServices] CandidaturaConverter converter, [FromServices] IUnitOfWork unitOfWork, CandidaturaRequest candidaturaRequest) =>
        {
            var candidatura = converter.RequestToEntity(candidaturaRequest);
             await unitOfWork.CandidaturaRepository.Adicionar(candidatura);
            await unitOfWork.Commit();

            return Results.Created($"/candidatura/{candidatura.Id}", candidatura);
        }).WithTags("Candidatura").WithOpenApi();

        app.MapPut("/candidatura/{id}", async ([FromServices] CandidaturaConverter converter, [FromServices] IUnitOfWork unitOfWork, Guid id, CandidaturaRequest candidaturaRequest) =>
        {
            var candidatura = await unitOfWork.CandidaturaRepository.BuscarPorId(x => x.Id == id);
            if (candidatura is null)
            {
                return Results.NotFound();
            }
            var candidaturaAtualizada = converter.RequestToEntity(candidaturaRequest);
            candidatura.Status = candidaturaAtualizada.Status;
            candidatura.ValorProposto = candidaturaAtualizada.ValorProposto;
            candidatura.DescricaoProposta = candidaturaAtualizada.DescricaoProposta;
            candidatura.DuracaoProposta = candidaturaAtualizada.DuracaoProposta;

            await unitOfWork.CandidaturaRepository.Atualizar(candidatura);
            await unitOfWork.Commit();

            return Results.Ok((candidatura));
        }).WithTags("Candidatura").WithOpenApi();

        app.MapDelete("/candidatura/{id}", async ([FromServices] CandidaturaConverter converter, [FromServices] IUnitOfWork unitOfWork, Guid id) =>
        {
            var candidatura = await unitOfWork.CandidaturaRepository.BuscarPorId(x => x.Id == id);
            if (candidatura is null)
            {
                return Results.NotFound();
            }

            await unitOfWork.CandidaturaRepository.Deletar(candidatura); 
            await unitOfWork.Commit();

            return Results.NoContent();
        }).WithTags("Candidatura").WithOpenApi();

    }
}
