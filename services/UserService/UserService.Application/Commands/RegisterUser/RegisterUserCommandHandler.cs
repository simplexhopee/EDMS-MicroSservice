using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Dtos;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Shared.MultiTenancy;

namespace UserService.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITenantContext _tenantContext;


        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITenantContext tenantContext)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _tenantContext = tenantContext;
        }

       

        public async Task<UserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.ExistsByEmailAndTenant(request.Email);
            if (exists == true)
            {

            }
            await _userRepository.AddAsync(new User
            {
                Name = request.Name,
                Email = request.Email,
                Surname = request.Surname,
                Phone = request.Phone,

            });
            return new UserResponse {Name = request.Name, Email = request.Email, Surname = request.Surname, Phone = request.Phone };
        }
    }
}
