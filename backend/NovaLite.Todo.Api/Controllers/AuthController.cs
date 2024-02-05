using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Shared.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Novalite.Todo.Shared.Model;
using Azure.Core;
using System.IdentityModel.Tokens.Jwt;
using Novalite.Todo.Api;

namespace NovaLite.Todo.Api.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]

    
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController( ILogger<AuthController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            
            string userEmailClaim = JwtTokenHelper.ExtractUsernameFromToken(accessToken);

            var user = await _unitOfWork.TodoUserRepository.GetByEmailAsync(userEmailClaim);
            if (user == null  && !userEmailClaim.IsNullOrEmpty())
            {
                var newUser = await _unitOfWork.TodoUserRepository.CreateAsync(new TodoUser()
                {
                    Email = userEmailClaim,
                    Role = Role.User
                });
                await _unitOfWork.CompleteAsync();
                return Ok(new { Role = newUser.Role.ToString() });
            }
            else
            {
                var existingUser = await _unitOfWork.TodoUserRepository.GetByEmailAsync(userEmailClaim);
                return Ok(new { Role = existingUser.Role.ToString() });
            }
            // Log or use the token as needed
            
        }
    }
}
