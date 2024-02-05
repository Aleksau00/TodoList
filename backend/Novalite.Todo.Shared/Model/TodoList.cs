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
        public TodoUser User { get; set; }
        public Guid UserId { get; set; }

        public List<TodoReminder> Reminders { get; set; } = new List<TodoReminder>();

        public List<TodoAttachment> Attachments { get; set; } = new List<TodoAttachment>();
    }
}
