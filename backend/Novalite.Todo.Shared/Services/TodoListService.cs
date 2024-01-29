using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.DTOs;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Shared.Services
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
            return await _unitOfWork.TodoListRepository.GetAllWithReminders();
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

        public async Task<TodoReminder?> CreateIfReminderViable(TodoReminder todoReminder)
        {
            var list = await _unitOfWork.TodoListRepository.GetByIdWithRemindersAsync(todoReminder.TodoListId);
            if (list.Reminders.Count == 0)
            {
                return CreateReminderTask(todoReminder, list).Result;
            }
            foreach (var todo in list.Reminders)
            {
                if (todo.Sent == false)
                {
                    return null;
                }
            }

            return CreateReminderTask(todoReminder, list).Result;
        }

        public async Task<TodoReminder> CreateReminderTask(TodoReminder todoReminder, TodoList list)
        {
            todoReminder.Sent = false;
            var newReminder = await _unitOfWork.TodoReminderRepository.CreateAsync(todoReminder);
            list.Reminders.Add(newReminder);
            await _unitOfWork.CompleteAsync();
            return newReminder;
        }
    }
}
