using NovaLite.Todo.Api.Data;
using NovaLite.Todo.Api.Model;
using System.Collections.Generic;

namespace NovaLite.Todo.Api.Repos.TodoListRepo
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetAllItems();
        Task<IEnumerable<TodoItem>> GetByListId(Guid givenId);
    }

}
