using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Dtos;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Shared.Database;
using UserService.Shared.Exceptions;
using UserService.Shared.MultiTenancy;


namespace UserService.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantContext _tenantContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserServices _userServices;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUserServices userServices,
            ITenantContext tenantContext, IUnitOfWork unitOfWork)
        {
             _userRepository = userRepository;
            _tenantContext = tenantContext;
            _unitOfWork = unitOfWork;
            _userServices = userServices;
        }

       

        public async Task<UserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var exists = await _userRepository.ExistsByEmailAndTenant(request.Email);
            if (exists == true)
            {
                throw new EntityConflictException(request.Email);
            }
            await _userServices.NewUser(new User
            {
                Name = request.Name,
                Email = request.Email,
                Surname = request.Surname,
                PhoneNumber = request.Phone,
                UserName = request.Email
            });
          
            return new UserResponse {Name = request.Name, Email = request.Email, Surname = request.Surname, Phone = request.Phone };
        }
    }
}
