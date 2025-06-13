using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Commands.Login;
using UserService.Application.Commands.NewPassword;
using UserService.Application.Dtos;
using UserService.Shared.Exceptions;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;   
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new LoginCommand(login.Email, login.Password));
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _mediator.Send(new ResetPasswordCommand(email));
                return Ok(new { message = "Reset Password linked sent to email" });
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("new-password")]
        public async Task<IActionResult> NewPassword([FromBody] NewPasswordDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _mediator.Send(new NewPasswordCommand(input.Email, input.Password, input.Token));
                return Ok(new { message = "Password Changed Successfully" });
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

       
    }
}
