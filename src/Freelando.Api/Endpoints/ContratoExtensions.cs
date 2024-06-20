using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;

public static class ContratoExtensions
{
    public static void AddEndPointContratos(this WebApplication app)
    {
        app.MapGet("/contratos", async ([FromServices] ContratoConverter converter, [FromServices] FreelandoContext contexto) =>
        {
            var contrato = converter.EntityListToResponseList(contexto.Contratos.ToList());

            var entries = contexto.ChangeTracker.Entries();

            return Results.Ok(await Task.FromResult(contrato));
        }).WithTags("Contrato").WithOpenApi();

        app.MapPost("/contrato", async ([FromServices] ContratoConverter converter, [FromServices] FreelandoContext contexto, ContratoRequest contratoRequest) =>
        {
            using var transaction = await contexto.Database.BeginTransactionAsync();
            try
            {
                transaction.CreateSavepoint("Savepoint");
                var contrato = converter.RequestToEntity(contratoRequest);
                await contexto.Contratos.AddAsync(contrato);
                await contexto.SaveChangesAsync();
                await transaction.CommitAsync();
                return Results.Created($"/contrato/{contrato.Id}", contrato);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Results.BadRequest($" Problemas de simultaneidade {ex.Message}");
            }
            catch (Exception ex)
            {

                transaction.RollbackToSavepoint("Savepoint");
                return Results.BadRequest(ex.Message);           
            }


        }).WithTags("Contrato").WithOpenApi();

        app.MapPut("/contrato/{id}", async ([FromServices] ContratoConverter converter, [FromServices] FreelandoContext contexto, Guid id, ContratoRequest contratoRequest) =>
        {
            var contrato = await contexto.Contratos.FindAsync(id);
            if (contrato is null)
            {
                return Results.NotFound();
            }
            var contratoAtualizado = converter.RequestToEntity(contratoRequest);
            contrato.Valor = contratoAtualizado.Valor;
            contrato.Vigencia = contratoAtualizado.Vigencia;

            await contexto.SaveChangesAsync();

            return Results.Ok((contrato));
        }).WithTags("Contrato").WithOpenApi();

        app.MapDelete("/contrato/{id}", async ([FromServices] ContratoConverter converter, [FromServices] FreelandoContext contexto, Guid id) =>
        {
            var contrato = await contexto.Contratos.FindAsync(id);
            if (contrato is null)
            {
                return Results.NotFound();
            }

            contexto.Contratos.Remove(contrato);
            await contexto.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Contrato").WithOpenApi();
    }
}
