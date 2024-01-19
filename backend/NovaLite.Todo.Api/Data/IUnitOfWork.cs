

using NovaLite.Todo.Api.Repos.TodoListRepo;

namespace NovaLite.Todo.Api.Data
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        ITodoListRepository TodoListRepository { get; }
        ITodoItemRepository TodoItemRepository { get; }
        T GetRepository<T>() where T : class;
        Task CompleteAsync();
    }
}
