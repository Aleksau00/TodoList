using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Shared.Model
{
    public class TodoAttachment
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public String FileName { get; set; }

        public TodoList TodoList { get; set; }
        public Guid TodoListId { get; set; }
       
    }
}
