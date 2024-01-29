using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Api.Mapper;
using Novalite.Todo.Shared.DTOs;
using NovaLite.Todo.Shared.DTOs;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;
using Novalite.Todo.Shared.Repos.TodoListRepo;
using NovaLite.Todo.Shared.Services;
using NovaLite.Todo.Shared.DTOs;

namespace NovaLite.Todo.Api.Controllers
{
    [Route("api/reminders")]
    [ApiController]
    public class TodoReminderController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoReminderRepository _todoReminderRepository;
        private readonly IMapper _mapper;

        public TodoReminderController(ITodoListService todoListService, ITodoReminderRepository todoReminderRepository)
        {
            _todoListService = todoListService;
            _todoReminderRepository = todoReminderRepository;
            _mapper = AutoMapperConfig.Configure();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TodoReminder>> CreateReminder([FromBody] TodoReminderRequest todoReminderRequest)
        {
            var todoReminder = _mapper.Map<TodoReminder>(todoReminderRequest);
            todoReminder.Timestamp = DateTimeOffset.UtcNow;
            var result = await _todoListService.CreateIfReminderViable(todoReminder);
            return result == null ? Conflict() : Created();

        }
    }
}
