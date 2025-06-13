using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Dtos;

namespace UserService.Application.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

}
