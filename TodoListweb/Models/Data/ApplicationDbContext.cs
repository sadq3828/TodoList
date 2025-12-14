using Microsoft.EntityFrameworkCore;

namespace TodoListweb.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Todo>Todos {get;set;}
    }
}
