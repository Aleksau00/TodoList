using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.Repos.TodoListRepo
{
    public class TodoUserRepository : GenericRepository<TodoUser>, ITodoUserRepository
    {
        public TodoUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

            
        }

        public async Task<TodoUser> GetByEmailAsync(string email)
        {
            return await DbSet
                .FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

    }
}
