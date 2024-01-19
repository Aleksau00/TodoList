

using NovaLite.Todo.Api.Repos.TodoListRepo;

namespace NovaLite.Todo.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private TodoListRepository _todoListRepository;
        private TodoItemRepository _todoItemRepository;
        private bool _disposed;

        public ITodoListRepository TodoListRepository => _todoListRepository ??= new TodoListRepository(_context);
        public ITodoItemRepository TodoItemRepository => _todoItemRepository ??= new TodoItemRepository(_context);

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();

        public T GetRepository<T>() where T : class
        {
            var result = (T)Activator.CreateInstance(typeof(T), _context);
            return result;
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _context?.Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}
