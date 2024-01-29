using NovaLite.Todo.Shared.Data;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Repos.TodoListRepo
{
    public interface ITodoListRepository : IGenericRepository<TodoList>
    {

        Task<TodoList> GetByIdWithRemindersAsync(Guid givenId);
        Task<IEnumerable<TodoList>> GetAllWithReminders();
    }
}
