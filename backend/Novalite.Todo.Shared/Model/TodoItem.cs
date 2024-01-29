namespace NovaLite.Todo.Shared.Model
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public Status Status { get; set; }
        public Guid TodoListId { get; set; }
        public required TodoList TodoList { get; set; }
    }
}
