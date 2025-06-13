using MediatR;

namespace UserService.Application.Commands
{
    public record ResetPasswordCommand(string email) : IRequest;
   
}