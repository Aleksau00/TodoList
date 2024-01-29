using NovaLite.Todo.Shared.Data;
using NovaLite.Todo.Shared.Model;
using System.Collections.Generic;

namespace NovaLite.Todo.Shared.Repos.TodoListRepo
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
        
        Task<IEnumerable<TodoItem>> GetByListId(Guid givenId);
    }

}
