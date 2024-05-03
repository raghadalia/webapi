using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
namespace ToDoApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ToDoApi; Trusted_Connection = True; MultipleActiveResultSets = true")
                .LogTo(Console.WriteLine,
                      new[] { DbLoggerCategory.Database.Command.Name },
                      LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        public DbSet<ToDos> ToDos { get; set; }
    }

}
