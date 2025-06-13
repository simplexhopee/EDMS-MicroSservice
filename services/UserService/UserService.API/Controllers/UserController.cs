using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Commands.Login;
using UserService.Application.Commands.RegisterUser;
using UserService.Application.Dtos;
using UserService.Shared.Exceptions;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;   
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new RegisterUserCommand(
               registerUser.Email, registerUser.Name, registerUser.Surname, registerUser.Phone));
                return Ok(result);
            }
            catch (EntityConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }

        }

    }
}
