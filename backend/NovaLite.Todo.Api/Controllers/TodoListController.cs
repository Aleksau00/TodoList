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
        private readonly IMapper _mapper;
        
        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoList>> GetById([FromRoute] Guid id)
        {
            var result = await _todoListService.GetById(id);
            return result == null ? NotFound() : Ok(result);
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
