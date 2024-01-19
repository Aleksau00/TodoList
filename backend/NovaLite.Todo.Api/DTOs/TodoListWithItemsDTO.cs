using Microsoft.AspNetCore.Mvc.ModelBinding;
using NovaLite.Todo.Api.Enums;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.DTOs
{
    public class TodoListWithItemsDTO
    {
        public required TodoList TodoList { get; set; }
        public IEnumerable<TodoItem>? TodoItems { get; set; }
    }
}
