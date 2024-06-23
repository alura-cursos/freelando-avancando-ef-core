using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;
public static class ProjetoExtensions
{
    public static void AddEndPointProjetos(this WebApplication app)
    {
        app.MapGet("/projetos", async ([FromServices] ProjetoConverter converter, [FromServices] IUnitOfWork unitOfOrk) =>
        {
            var projetos = converter.EntityListToResponseList(unitOfOrk.contexto.Projetos.Include(p => p.Cliente).Include(p => p.Especialidades).ToList());

            return Results.Ok(await Task.FromResult(projetos));
        }).WithTags("Projeto").WithOpenApi();

        app.MapGet("/projetos/vigencia", async ([FromServices] ProjetoConverter converter, [FromServices] IUnitOfWork unitOfOrk) =>
        {
            var projetos = unitOfOrk.ProjetoRepository.BuscarTodos();

            return Results.Ok(await Task.FromResult(projetos));
        }).WithTags("Projeto").WithOpenApi();

        app.MapPost("/projeto", async ([FromServices] ProjetoConverter converter, [FromServices] IUnitOfWork unitOfOrk, ProjetoRequest projetoRequest) =>
        {
            var projeto = converter.RequestToEntity(projetoRequest);

            await unitOfOrk.ProjetoRepository.Adicionar(projeto);
            await unitOfOrk.Commit();

            return Results.Created($"/projeto/{projeto.Id}", projeto);
        }).WithTags("Projeto").WithOpenApi();

        app.MapPut("/projeto/{id}", async ([FromServices] ProjetoConverter converter, [FromServices] IUnitOfWork unitOfOrk, Guid id, ProjetoRequest projetoRequest) =>
        {
            var projeto = await unitOfOrk.ProjetoRepository.BuscarPorId(x=>x.Id==id);
            if (projeto is null)
            {
                return Results.NotFound();
            }
            var projetoAtualizado = converter.RequestToEntity(projetoRequest);
            projeto.Titulo = projetoAtualizado.Titulo;
            projeto.Descricao = projetoAtualizado.Descricao;
            projeto.Status = projetoAtualizado.Status;

            await unitOfOrk.ProjetoRepository.Atualizar(projeto);
            await unitOfOrk.Commit();

            return Results.Ok((projeto));
        }).WithTags("Projeto").WithOpenApi();

        app.MapDelete("/projeto/{id}", async ([FromServices] ProjetoConverter converter, [FromServices] IUnitOfWork unitOfOrk, Guid id) =>
        {
            var projeto = await unitOfOrk.ProjetoRepository.BuscarPorId(x => x.Id == id);
            if (projeto is null)
            {
                return Results.NotFound();
            }

            await unitOfOrk.ProjetoRepository.Deletar(projeto);
            await unitOfOrk.Commit();

            return Results.NoContent();
        }).WithTags("Projeto").WithOpenApi();
    }
}
