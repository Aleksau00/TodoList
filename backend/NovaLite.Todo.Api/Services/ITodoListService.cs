using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.Services
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoList>> GetAll();
        Task<TodoList> Create(TodoList todoList);
        Task<TodoList> GetById(Guid id);
        Task<bool> Update(TodoList todoList);
    }
}
