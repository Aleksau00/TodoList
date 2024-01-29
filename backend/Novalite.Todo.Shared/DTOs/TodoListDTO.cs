using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NovaLite.Todo.Shared.DTOs
{
    public class TodoListDTO
    {
        public string? Title { get; set; }  
        public string? Description { get; set; }
    }
}
