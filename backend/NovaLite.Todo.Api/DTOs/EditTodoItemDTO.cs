using NovaLite.Todo.Api.Enums;

namespace NovaLite.Todo.Api.DTOs
{
    public class EditTodoItemDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Status Status { get; set; }
    }
}
