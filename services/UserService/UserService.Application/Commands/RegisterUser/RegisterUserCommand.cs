using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Dtos;

namespace UserService.Application.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Name, string Surname, string Phone) : IRequest<UserResponse>;
    
}
