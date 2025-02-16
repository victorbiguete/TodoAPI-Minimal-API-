using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;

namespace TodoAPI.Estudantes
{
    public static class EstudantesRotas
    {
        public static void AddRotasEstudantes(this WebApplication app)
        {
            var rotasEstudantes = app.MapGroup("estudantes");

            //Criar Estudante
            rotasEstudantes.MapPost("" ,async (AddEstudanteRequest request,AppDbContext context)=>
            {
                var _errors = await context.Estudantes.AnyAsync(estudante => request.Nome.Equals(estudante.Nome));

                if (_errors)
                    return Results.Conflict("Nome já existe");

                var novoEstudante = new Estudante(request.Nome);

                await context.Estudantes.AddAsync(novoEstudante);
                await context.SaveChangesAsync();

                return Results.Ok();
            });

            rotasEstudantes.MapGet("", async());
        }
    }
}
