using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.RegisterUser;
using UserService.Application.Dtos;

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
            var result = await _mediator.Send(new RegisterUserCommand(
                registerUser.Email, registerUser.Name, registerUser.Surname, registerUser.Phone));
            return Ok(result);
        }
    }
}
