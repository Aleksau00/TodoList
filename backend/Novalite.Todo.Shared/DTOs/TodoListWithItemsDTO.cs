using Microsoft.AspNetCore.Mvc.ModelBinding;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.DTOs
{
    public class TodoListWithItemsDTO
    {
        public required TodoList TodoList { get; set; }
        public IEnumerable<TodoItem>? TodoItems { get; set; }
    }
}
