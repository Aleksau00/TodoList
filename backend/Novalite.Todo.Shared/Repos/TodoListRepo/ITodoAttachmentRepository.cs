using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaLite.Todo.Shared.Data;
using NovaLite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.Repos.TodoListRepo
{
    public interface ITodoAttachmentRepository : IGenericRepository<TodoAttachment>
    {
         Task<IEnumerable<TodoAttachment>> GetAllFromListAsync(Guid todoListId);
    }
}
