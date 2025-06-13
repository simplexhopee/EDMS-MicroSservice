using MediatR;

namespace UserService.Application.Commands.NewPassword
{
    public record NewPasswordCommand(string Email, string Password, string Token) : IRequest;
    
}