using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Shared.DTOs;
using NovaLite.Todo.Api.Mapper;
using NovaLite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Api;
using Novalite.Todo.Shared.DTOs;

namespace NovaLite.Todo.Api.Controller
{
    [Authorize(Policy = "User")]
    [ApiController]
    [Route("api/lists")]
    
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoListController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public TodoListController(IUnitOfWork unit, ITodoListService todoListService, ITodoItemService todoItemService, ILogger<TodoListController> logger, IHttpContextAccessor contextAccessor)
        {
            _todoListService = todoListService;
            _todoItemService = todoItemService;
            _mapper = AutoMapperConfig.Configure();
            _logger = logger;
            _contextAccessor = contextAccessor;
            _unitOfWork = unit;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetAll()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userEmailClaim = JwtTokenHelper.ExtractUsernameFromToken(accessToken);
            var user = await _unitOfWork.TodoUserRepository.GetByEmailAsync(userEmailClaim);
            var todoLists = await _unitOfWork.TodoListRepository.GetAllWithRemindersFromUser(user.Id);
            return Ok(todoLists);
        }

        [HttpPost]
        public async Task<ActionResult<TodoList>> CreateTodoList([FromBody] TodoListDTO todoListDTO)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userEmailClaim = JwtTokenHelper.ExtractUsernameFromToken(accessToken);
            var user = await _unitOfWork.TodoUserRepository.GetByEmailAsync(userEmailClaim);
            var todoList = _mapper.Map<TodoList>(todoListDTO);
            todoList.UserId = user.Id;
            var result = await _todoListService.Create(todoList);
            return Created();
        }

        [HttpPost("item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoList>> CreateTodoItem([FromBody] TodoItemDTO todoItemDTO)
        {
            var todoitem = _mapper.Map<TodoItem>(todoItemDTO);
            var result = await _todoItemService.Create(todoitem);
            return result == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoListWithItemsDTO>> GetById([FromRoute] Guid id)
        {
            var list = await _todoListService.GetById(id);
            var items = await _todoItemService.GetByListId(id);
            if (list == null)
            {
                return NotFound();
            }

            TodoListWithItemsDTO result = new TodoListWithItemsDTO
            {
                TodoList = list,
                TodoItems = items
            };

            return Ok(result);
        }

        [HttpPut("item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTodoItem([FromBody] EditTodoItemDTO todoItemDTO)
        {

            var result = await _todoItemService.Update(todoItemDTO);
            return result == false ? NotFound() : Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTodoList([FromBody] TodoListRequest todoList)
        {
            var list = await _unitOfWork.TodoListRepository.GetByIdAsync(todoList.Id);
            list.Title = todoList.Title;
            list.Description = todoList.Description;
            await _unitOfWork.TodoListRepository.UpdateAsync(list);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}
