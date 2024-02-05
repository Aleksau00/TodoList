using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novalite.Todo.Shared.Model;

namespace Novalite.Todo.Shared.DTOs
{
    public class TodoUserRequest
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
