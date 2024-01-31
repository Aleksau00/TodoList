using NovaLite.Todo.Shared.Data;
using NovaLite.Todo.Shared.Model;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Novalite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Repos.TodoListRepo
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TodoList> GetByIdWithRemindersAsync(Guid givenId)
        {
            return await DbSet
                    .Include(todoList => todoList.Reminders)
                    .Select(todoList => new TodoList
                    {
                        Id = todoList.Id,
                        Title = todoList.Title,
                        Description = todoList.Description,
                        Reminders = todoList.Reminders.Select(reminder => new TodoReminder
                        {
                            Id = reminder.Id,
                            Timestamp = reminder.Timestamp,
                            Sent = reminder.Sent,
                            TodoListId = reminder.TodoListId
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(todoList => todoList.Id == givenId);
        }

        public async Task<IEnumerable<TodoList>> GetAllWithReminders()
        {
            return await DbSet
                .Include(todoList => todoList.Reminders)
                .Select(todoList => new TodoList
                {
                    Id = todoList.Id,
                    Title = todoList.Title,
                    Description = todoList.Description,
                    Reminders = todoList.Reminders.Select(reminder => new TodoReminder
                    {
                        Id = reminder.Id,
                        Timestamp = reminder.Timestamp,
                        Sent = reminder.Sent,
                        TodoListId = reminder.TodoListId
                        // Exclude TodoList property
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
