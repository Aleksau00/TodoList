

using Novalite.Todo.Shared.Repos.TodoListRepo;
using NovaLite.Todo.Shared.Repos.TodoListRepo;

namespace NovaLite.Todo.Shared.Data
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        ITodoListRepository TodoListRepository { get; }
        ITodoItemRepository TodoItemRepository { get; }
        ITodoReminderRepository TodoReminderRepository { get; }
        ITodoAttachmentRepository TodoAttachmentRepository { get; }
        ITodoUserRepository TodoUserRepository { get; }
        T GetRepository<T>() where T : class;
        Task CompleteAsync();
    }
}
