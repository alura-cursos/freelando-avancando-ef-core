using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;

public static class ContratoExtensions
{
    public static void AddEndPointContratos(this WebApplication app)
    {
        app.MapGet("/contratos", async ([FromServices] ContratoConverter converter, [FromServices] IUnitOfWork unitOfWork) =>
        {
            var contrato = converter.EntityListToResponseList(await unitOfWork.ContratoRepository.BuscarTodos());

            return Results.Ok(await Task.FromResult(contrato));
        }).WithTags("Contrato").WithOpenApi();

        app.MapPost("/contrato", async ([FromServices] ContratoConverter converter, [FromServices] IUnitOfWork unitOfWork, ContratoRequest contratoRequest) =>
        {
            using var transaction = await unitOfWork.contexto.Database.BeginTransactionAsync();
            try
            {
                transaction.CreateSavepoint("Savepoint");
                var contrato = converter.RequestToEntity(contratoRequest);
                await unitOfWork.contexto.Contratos.AddAsync(contrato);
                await unitOfWork.contexto.SaveChangesAsync();
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

        app.MapPut("/contrato/{id}", async ([FromServices] ContratoConverter converter, [FromServices] IUnitOfWork unitOfWork, Guid id, ContratoRequest contratoRequest) =>
        {
            var contrato = await unitOfWork.ContratoRepository.BuscarPorId(x => x.Id == id);
            if (contrato is null)
            {
                return Results.NotFound();
            }
            var contratoAtualizado = converter.RequestToEntity(contratoRequest);
            contrato.Valor = contratoAtualizado.Valor;
            contrato.Vigencia = contratoAtualizado.Vigencia;

            await unitOfWork.ContratoRepository.Atualizar(contrato);
            await unitOfWork.Commit();

            return Results.Ok((contrato));
        }).WithTags("Contrato").WithOpenApi();

        app.MapDelete("/contrato/{id}", async ([FromServices] ContratoConverter converter, [FromServices] IUnitOfWork unitOfWork, Guid id) =>
        {
            var contrato = await unitOfWork.ContratoRepository.BuscarPorId(x => x.Id == id);
            if (contrato is null)
            {
                return Results.NotFound();
            }

            await unitOfWork.ContratoRepository.Deletar(contrato);
            await unitOfWork.Commit();

            return Results.NoContent();
        }).WithTags("Contrato").WithOpenApi();
    }
}
