using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.DTOs
{
    public class EditTodoItemDTO
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public Status Status { get; set; }
    }
}
