using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.DTOs
{
    public class EditTodoItemDTO
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public Status Status { get; set; }
    }
}
