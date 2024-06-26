using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados.UnitOfWork;
using Freelando.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        app.MapPost("/projetos-proposta", async ([FromServices] ProjetoConverter converter, [FromServices] IUnitOfWork unitOfWork, Propostas proposta) =>
        {

            Guid idProposta = Guid.NewGuid();

            string sql = "EXEC dbo.sp_InserirProposta @Id_Proposta, @Id_Projeto, @Id_Profissional, @Valor_Proposta, @Prazo_Entrega, @Mensagem";


            object[] parametros = new object[]
            {
                new SqlParameter("@Id_Proposta", idProposta),
                new SqlParameter("@Id_Projeto", proposta.ProjetoId),
                new SqlParameter("@Id_Profissional", proposta.ProfissionalId),
                new SqlParameter("@Valor_Proposta", proposta.ValorProposta),
                new SqlParameter("@Prazo_Entrega", proposta.PrazoEntrega),
                new SqlParameter("@Mensagem", proposta.Mensagem ?? (object)DBNull.Value)
            };


            await unitOfWork.contexto.Database.ExecuteSqlRawAsync(sql, parametros);

            return Results.Ok();
        }).WithTags("Projeto").WithOpenApi();



    }
}
