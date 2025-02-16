using Microsoft.EntityFrameworkCore;
using TodoAPI.Estudantes;

namespace TodoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Estudante> Estudantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
            optionsBuilder.LogTo(Console.WriteLine,LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
