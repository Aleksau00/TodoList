using NovaLite.Todo.Api.Data;
using NovaLite.Todo.Api.Model;
using System.Diagnostics;

namespace NovaLite.Todo.Api.Repos.TodoListRepo
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
