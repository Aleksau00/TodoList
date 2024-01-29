using NovaLite.Todo.Shared.DTOs;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> Create(TodoItem todoItem);
        //Task<TodoItem> GetById(Guid id);
        Task<bool> Update(EditTodoItemDTO todoItemDTO);
        Task <IEnumerable<TodoItem>> GetByListId(Guid id);
    }
}
