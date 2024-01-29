using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.Repos.TodoListRepo
{
    public class TodoReminderRepository : GenericRepository<TodoReminder>, ITodoReminderRepository
    {

        public TodoReminderRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<TodoReminder>> GetAllItems()
        {
            return await DbSet.Include(x => x.TodoList)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoReminder>> GetByListId(Guid givenId)
        {
            return await DbSet
                .Where(todoItem => todoItem.TodoListId == givenId)
                .ToListAsync();
        }

    }
}
