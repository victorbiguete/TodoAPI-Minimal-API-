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

                var estudanteRetorno = new EstudanteDTO(novoEstudante.Id, novoEstudante.Nome);

                return Results.Ok(estudanteRetorno);
            });

            //Listar Estudante Ativo
            rotasEstudantes.MapGet("", async(AppDbContext context) =>
            {
                var estudantes = await context.Estudantes
                .Where(estudante=>estudante.Ativo)
                .Select(estudante => new EstudanteDTO(estudante.Id,estudante.Nome))
                .ToListAsync();
                
                return estudantes;
            });

            //Atualizar Estudante
            rotasEstudantes.MapPut("{id:guid}", async (Guid id,UpdateEstudanteRequest request, AppDbContext context) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

                if (estudante == null)
                    return Results.NotFound();

                estudante.AtualizaNome(request.Nome);

                await context.SaveChangesAsync();

                return Results.Ok(new EstudanteDTO(estudante.Id,estudante.Nome);
            });
        }
    }
}
