using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Shared.Data;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Repos.TodoListRepo
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
