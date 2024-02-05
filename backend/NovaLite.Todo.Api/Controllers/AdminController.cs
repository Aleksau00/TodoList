using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.Model;
using Novalite.Todo.Api;
using Novalite.Todo.Shared.DTOs;
using NovaLite.Todo.Shared.Model;
using NovaLite.Todo.Shared.DTOs;

namespace NovaLite.Todo.Api.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoUser>>> GetAll()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userEmailClaim = JwtTokenHelper.ExtractUsernameFromToken(accessToken);
            var user = await _unitOfWork.TodoUserRepository.GetByEmailAsync(userEmailClaim);
            if (user.Role != Role.Admin)
            {
                return Unauthorized();
            }
            var users= await _unitOfWork.TodoUserRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser([FromBody] TodoUserRequest request)
        {

            var currentUser = await _unitOfWork.TodoUserRepository.GetByIdAsync(request.Id);
            currentUser.Role = request.Role;
            await _unitOfWork.TodoUserRepository.UpdateAsync(currentUser);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}
