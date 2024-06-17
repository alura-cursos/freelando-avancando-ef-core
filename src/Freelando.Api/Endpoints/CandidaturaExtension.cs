using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados;
using Freelando.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;

public static class CandidaturaExtension
{
    public static void AddEndPointCandidaturas(this WebApplication app)
    {
        app.MapGet("/candidaturas", async ([FromServices] CandidaturaConverter converter, [FromServices] FreelandoContext contexto) =>
        {
            var candidatura = converter.EntityListToResponseList(contexto.Candidaturas.AsNoTracking().ToList());

            var entries = contexto.ChangeTracker.Entries();

            return Results.Ok(await Task.FromResult(candidatura));
        }).WithTags("Candidatura").WithOpenApi();

        app.MapPost("/candidatura", async ([FromServices] CandidaturaConverter converter, [FromServices] FreelandoContext contexto, CandidaturaRequest candidaturaRequest) =>
        {
            var candidatura = converter.RequestToEntity(candidaturaRequest);

            await contexto.Candidaturas.AddAsync(candidatura);
            await contexto.SaveChangesAsync();

            return Results.Created($"/candidatura/{candidatura.Id}", candidatura);
        }).WithTags("Candidatura").WithOpenApi();

        app.MapPut("/candidatura/{id}", async ([FromServices] CandidaturaConverter converter, [FromServices] FreelandoContext contexto, Guid id, CandidaturaRequest candidaturaRequest) =>
        {
            var candidatura = await contexto.Candidaturas.FindAsync(id);
            if (candidatura is null)
            {
                return Results.NotFound();
            }
            var candidaturaAtualizada = converter.RequestToEntity(candidaturaRequest);
            candidatura.Status = candidaturaAtualizada.Status;
            candidatura.ValorProposto = candidaturaAtualizada.ValorProposto;
            candidatura.DescricaoProposta = candidaturaAtualizada.DescricaoProposta;
            candidatura.DuracaoProposta = candidaturaAtualizada.DuracaoProposta;

            await contexto.SaveChangesAsync();

            return Results.Ok((candidatura));
        }).WithTags("Candidatura").WithOpenApi();

        app.MapDelete("/candidatura/{id}", async ([FromServices] CandidaturaConverter converter, [FromServices] FreelandoContext contexto, Guid id) =>
        {
            var candidatura = await contexto.Candidaturas.FindAsync(id);
            if (candidatura is null)
            {
                return Results.NotFound();
            }

            contexto.Candidaturas.Remove(candidatura);
            await contexto.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Candidatura").WithOpenApi();

    }
}
