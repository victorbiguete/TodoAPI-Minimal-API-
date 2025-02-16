namespace TodoAPI.Estudantes
{
    public static class EstudantesRotas
    {
        public static void AddRotasEstudantes(this WebApplication app)
        {
            app.MapGet("estudantes", () => new Estudante("Victor"));
        }
    }
}
