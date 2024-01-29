using Microsoft.AspNetCore.Mvc.ModelBinding;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.DTOs
{
    public class TodoItemDTO
    { 
        public required string Content { get; set; }
        public Guid TodoListId { get; set; }
        public Status Status { get; set; }
    }
}
