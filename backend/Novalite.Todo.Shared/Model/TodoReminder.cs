using NovaLite.Todo.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novalite.Todo.Shared.Model
{
    public class TodoReminder
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Boolean Sent { get; set; }
        public Guid TodoListId { get; set; }  
        public TodoList? TodoList { get; set; } 


    }
}
