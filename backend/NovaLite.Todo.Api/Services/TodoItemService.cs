using NovaLite.Todo.Api.Data;
using NovaLite.Todo.Api.DTOs;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork _unitOfWork; 

        public TodoItemService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TodoItem?> Create(TodoItem todoItem)
        {
            var listExists = false;
            var todoLists = await _unitOfWork.TodoListRepository.GetAllAsync();
            foreach (var todo in todoLists)
            {
                if (todoItem.TodoListId == todo.Id)
                {
                    listExists = true;
                }
            }
            if (listExists == false) 
            {
                return null;
            }
            var newItem = await _unitOfWork.TodoItemRepository.CreateAsync(todoItem);
            await _unitOfWork.CompleteAsync();
            return newItem;
        }


        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _unitOfWork.TodoItemRepository.GetAllAsync();
        }

        //public async Task<TodoItem> GetById(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<TodoItem>> GetByListId(Guid id)
        {
            return await _unitOfWork.TodoItemRepository.GetByListId(id);
        }

        public async Task<bool> Update(EditTodoItemDTO todoItemDTO)
        {
            var todoItem = await _unitOfWork.TodoItemRepository.GetByIdAsync(todoItemDTO.Id);
            await _unitOfWork.TodoItemRepository.UpdateAsync(todoItem);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
