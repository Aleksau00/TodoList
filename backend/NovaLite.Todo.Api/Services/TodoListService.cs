using NovaLite.Todo.Api.Data;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoList> Create(TodoList todoList)
        {
            var newList = await _unitOfWork.TodoListRepository.CreateAsync(todoList);
            await _unitOfWork.CompleteAsync();
            return newList;
        }

        public async Task<IEnumerable<TodoList>> GetAll()
        {
            return await _unitOfWork.TodoListRepository.GetAllAsync();
        }

        public async Task<TodoList> GetById(Guid id)
        {
            Console.WriteLine(id);
            var todoList = await _unitOfWork.TodoListRepository.GetByIdAsync(id);
            Console.WriteLine(todoList);
            await _unitOfWork.CompleteAsync();
            return todoList;
        }

        public async Task<bool> Update(TodoList todoList)
        {
            await _unitOfWork.TodoListRepository.UpdateAsync(todoList);
            await _unitOfWork.CompleteAsync();
            return true;
            
        }
    }
}
