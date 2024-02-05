using Microsoft.EntityFrameworkCore;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoReminder> TodoReminders { get; set; }
        public DbSet<TodoAttachment> TodoAttachments { get; set; }
        public DbSet<TodoUser> TodoUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>()
                .HasMany(list => list.Reminders)
                .WithOne(reminder => reminder.TodoList)
                .HasForeignKey(reminder => reminder.TodoListId)
                .HasForeignKey(attachment => attachment.TodoListId);
        }
    }
}
