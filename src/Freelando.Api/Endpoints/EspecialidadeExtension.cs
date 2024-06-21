
using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados;
using Freelando.Dados.Repository;
using Freelando.Dados.UnitOfWork;
using Freelando.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Freelando.Api.Endpoints;

public static class EspecialidadeExtensions
{
    public static void AddEndPointEspecialidades(this WebApplication app)
    {
        app.MapGet("/especialidades", async ([FromServices] EspecialidadeConverter converter, [FromServices] IEspecialidadeRepository repository) =>
        {
            var especialidades = converter.EntityListToResponseList(await repository.BuscarTodos());


            return Results.Ok((especialidades));
        }).WithTags("Especialidade").WithOpenApi();

        app.MapGet("/especialidades/{letraInicial}", async ([FromServices] EspecialidadeConverter converter, [FromServices] FreelandoContext contexto, string letraInicial) =>
        {
            Expression<Func<Especialidade, bool>> filtroExpression = null;

            if (letraInicial.Length == 1 && char.IsUpper(letraInicial[0]))
            {
                filtroExpression = especialidade => especialidade.Descricao.StartsWith(letraInicial);
            }

            IQueryable<Especialidade> especialidades = contexto.Especialidades;
            if(filtroExpression != null)
            {
                especialidades = especialidades.Where(filtroExpression);
            }

            return await especialidades.ToListAsync();
        }).WithTags("Especialidade").WithOpenApi();

        app.MapPost("/especialidade", async ([FromServices] EspecialidadeConverter converter, [FromServices] IUnitOfWork unitOfWork, EspecialidadeRequest especialidadeRequest) =>
        {
            var especialidade = converter.RequestToEntity(especialidadeRequest);

            Func<Especialidade, bool> validarDescricao = especialidade => !string.IsNullOrEmpty(especialidade.Descricao) && char.IsUpper(especialidade.Descricao[0]);

            if (!validarDescricao(especialidade))
            {
                return Results.BadRequest("A descrição não pode estar em branco e deve começar com letra maiúscula.");
            }

            await unitOfWork.EspecialidadeRepository.Adicionar(especialidade);
            await unitOfWork.Commit();
           
            return Results.Created($"/especialidade/{especialidade.Id}", especialidade);
        }).WithTags("Especialidade").WithOpenApi();

        app.MapPut("/especialidade/{id}", async ([FromServices] EspecialidadeConverter converter, [FromServices] FreelandoContext contexto, Guid id, EspecialidadeRequest especialidadeRequest) =>
        {
            var especialidade = await contexto.Especialidades.FindAsync(id);
            if (especialidade is null)
            {
                return Results.NotFound();
            }
            var especialidadeAtualizada = converter.RequestToEntity(especialidadeRequest);
            especialidade.Descricao = especialidadeAtualizada.Descricao;
            especialidade.Projetos = especialidadeAtualizada.Projetos;

            await contexto.SaveChangesAsync();

            return Results.Ok((especialidade));
        }).WithTags("Especialidade").WithOpenApi();

        /*app.MapDelete("/especialidade/{id}", async ([FromServices] EspecialidadeConverter converter, [FromServices] FreelandoContext contexto, Guid id) =>
        {
            var especialidade = await contexto.Especialidades.FindAsync(id);
            if (especialidade is null)
            {
                return Results.NotFound();
            }

            contexto.Especialidades.Remove(especialidade);
            await contexto.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Especialidade").WithOpenApi();*/

        app.MapDelete("/especialidade/{id}", async ([FromServices] EspecialidadeConverter converter, [FromServices] FreelandoContext contexto, Guid id) =>
        {
            using (var trasaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    var especialidade = await contexto.Especialidades.FindAsync(id);
                    if (especialidade is null)
                    {
                        return Results.NotFound();
                    }

                    contexto.Especialidades.Remove(especialidade);
                    await contexto.SaveChangesAsync();

                    trasaction.Commit();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    trasaction.Rollback();
                    throw ex;
                }
                
            }
        }).WithTags("Especialidade").WithOpenApi();
    }
}



