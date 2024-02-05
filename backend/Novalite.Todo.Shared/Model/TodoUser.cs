using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaLite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.Model
{
    public class TodoUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public List<TodoList> lists { get; set; }
    }
}
