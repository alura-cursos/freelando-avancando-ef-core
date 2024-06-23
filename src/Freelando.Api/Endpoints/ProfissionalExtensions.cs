using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;

public static class ProfissionalExtensions
{
    public static void AddEndPointProfissionais(this WebApplication app)
    {
        app.MapGet("/profissionais", async ([FromServices] ProfissionalConverter converter, [FromServices] IUnitOfWork unitOfOrk) =>
        {
            var profissional = converter.EntityListToResponseList(unitOfOrk.contexto.Profissionais.Include(e => e.Especialidades).ToList());

            var entries = unitOfOrk.contexto.ChangeTracker.Entries();

            return Results.Ok(await Task.FromResult(profissional));
        }).WithTags("Profissional").WithOpenApi();

        app.MapPost("/profissional", async ([FromServices] ProfissionalConverter converter, [FromServices] IUnitOfWork unitOfOrk, ProfissionalRequest profissionalRequest) =>
        {
            var profissional = converter.RequestToEntity(profissionalRequest);

            await unitOfOrk.ProfissionalRepository.Adicionar(profissional);
            await unitOfOrk.Commit();

            return Results.Created($"/profissional/{profissional.Id}", profissional);
        }).WithTags("Profissional").WithOpenApi();

        app.MapPut("/profissional/{id}", async ([FromServices] ProfissionalConverter converter, [FromServices] IUnitOfWork unitOfOrk, Guid id, ProfissionalRequest profissionalRequest) =>
        {
            var profissional = await unitOfOrk.ProfissionalRepository.BuscarPorId(x => x.Id == id);

            if (profissional is null)
            {
                return Results.NotFound();
            }
            var profissionalAtualizado = converter.RequestToEntity(profissionalRequest);
            profissional.Nome = profissionalAtualizado.Nome;
            profissional.Cpf = profissionalAtualizado.Cpf;
            profissional.Email = profissionalAtualizado.Email;
            profissional.Telefone = profissionalAtualizado.Telefone;

            await unitOfOrk.ProfissionalRepository.Atualizar(profissional);
            await unitOfOrk.Commit();

            return Results.Ok((profissional));
        }).WithTags("Profissional").WithOpenApi();

        app.MapDelete("/profissional/{id}", async ([FromServices] ProfissionalConverter converter, [FromServices] IUnitOfWork unitOfWork, Guid id) =>
        {
            var profissional = await unitOfWork.ProfissionalRepository.BuscarPorId(x => x.Id == id);
            if (profissional is null)
            {
                return Results.NotFound();
            }

            await unitOfWork.ProfissionalRepository.Deletar(profissional);
            await unitOfWork.Commit();

            return Results.NoContent();
        }).WithTags("Profissional").WithOpenApi();
    }
}
