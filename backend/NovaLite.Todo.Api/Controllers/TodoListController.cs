using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Api.DTOs;
using NovaLite.Todo.Api.Mapper;
using NovaLite.Todo.Api.Model;
using NovaLite.Todo.Api.Services;

namespace NovaLite.Todo.Api.Controller
{
    [ApiController]
    [Route("api/lists")]
    
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;
        
        public TodoListController(ITodoListService todoListService, ITodoItemService todoItemService)
        {
            _todoListService = todoListService;
            _todoItemService = todoItemService;
            _mapper = AutoMapperConfig.Configure();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetAll()
        {
            var todoLists = await _todoListService.GetAll();
            return Ok(todoLists);
        }

        [HttpPost]
        public async Task<ActionResult<TodoList>> CreateTodoList([FromBody] TodoListDTO todoListDTO)
        {
            var todoList = _mapper.Map<TodoList>(todoListDTO);
            var result = await _todoListService.Create(todoList);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("item")]
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
            return result == false ? NotFound() : NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTodoList([FromBody] TodoList todoList)
        {
            var result = await _todoListService.Update(todoList);
            return result == false ? NotFound() : NoContent();
        }

    }
}
