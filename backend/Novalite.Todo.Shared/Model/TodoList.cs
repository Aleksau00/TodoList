using Novalite.Todo.Shared.Model;
using System.ComponentModel.DataAnnotations;

namespace NovaLite.Todo.Shared.Model
{
    public class TodoList
    {
        public Guid Id { get; set; }
        [MaxLength(int.MaxValue)]
        public string Title { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; } = string.Empty;

        public List<TodoReminder> Reminders { get; set; } = new List<TodoReminder>();
    }
}
