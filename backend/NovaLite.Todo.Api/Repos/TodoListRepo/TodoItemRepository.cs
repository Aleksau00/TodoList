using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Api.Data;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.Repos.TodoListRepo
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TodoItem>> GetAllItems()
        {
            return await DbSet.Include(x => x.TodoList)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetByListId(Guid givenId)
        {
            return await DbSet
                .Where(todoItem => todoItem.TodoListId == givenId)
                .ToListAsync(); 
        }
    }
}
