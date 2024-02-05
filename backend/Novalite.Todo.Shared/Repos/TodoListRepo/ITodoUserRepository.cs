using NovaLite.Todo.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.Repos.TodoListRepo
{
    public interface ITodoUserRepository : IGenericRepository<TodoUser>
    {
        Task<TodoUser> GetByEmailAsync(string email);
    }
}
