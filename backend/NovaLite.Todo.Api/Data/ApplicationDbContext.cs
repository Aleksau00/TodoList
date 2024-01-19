using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoList> TodoLists { get; set; }
    }
}
