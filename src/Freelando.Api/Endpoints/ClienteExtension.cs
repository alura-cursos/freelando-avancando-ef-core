using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;

public static class ClienteExtension
{
    public static void AddEndPointClientes(this WebApplication app)
    {
        app.MapGet("/clientes", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto) =>
        {
            var clientes = converter.EntityListToResponseList(contexto.Clientes.ToList());

            return Results.Ok(await Task.FromResult(clientes));
        }).WithTags("Cliente").WithOpenApi();

        app.MapPost("/cliente", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto, ClienteRequest clienteRequest) =>
        {
            var cliente = converter.RequestToEntity(clienteRequest);

            await contexto.Clientes.AddAsync(cliente);
            await contexto.SaveChangesAsync();

            return Results.Created($"/cliente/{cliente.Id}", cliente);
        }).WithTags("Cliente").WithOpenApi();

        app.MapPut("/cliente/{id}", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto, Guid id, ClienteRequest clienteRequest) =>
        {
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente is null)
            {
                return Results.NotFound();
            }
            var clienteAtualizado = converter.RequestToEntity(clienteRequest);
            cliente.Nome = clienteAtualizado.Nome;
            cliente.Cpf = clienteAtualizado.Cpf;
            cliente.Email = clienteAtualizado.Email;
            cliente.Telefone = clienteAtualizado.Telefone;

            await contexto.SaveChangesAsync();

            return Results.Ok((cliente));
        }).WithTags("Cliente").WithOpenApi();

        app.MapDelete("/cliente/{id}", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto, Guid id) =>
        {
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente is null)
            {
                return Results.NotFound();
            }

            contexto.Clientes.Remove(cliente);
            await contexto.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Cliente").WithOpenApi();

    }
}
