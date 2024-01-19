using NovaLite.Todo.Api.DTOs;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.Services
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
