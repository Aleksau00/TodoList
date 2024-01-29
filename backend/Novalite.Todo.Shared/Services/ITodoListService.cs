using Novalite.Todo.Shared.DTOs;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Services
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoList>> GetAll();
        Task<TodoList> Create(TodoList todoList);
        Task<TodoList> GetById(Guid id);
        Task<bool> Update(TodoList todoList);
        Task<TodoReminder> CreateIfReminderViable(TodoReminder todoReminder);
    }
}
