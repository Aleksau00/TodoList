using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Shared.Data;
using NovaLite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.Repos.TodoListRepo
{
    public class TodoAttachmentRepository : GenericRepository<TodoAttachment>, ITodoAttachmentRepository
    {
        public TodoAttachmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TodoAttachment>> GetAllFromListAsync(Guid todoListId)
        {
            return await DbSet
                .Where(attachment => attachment.TodoListId == todoListId)
                .ToListAsync();
        }
    }
}
