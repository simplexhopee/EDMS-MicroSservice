using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Dtos;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IUserServices _userServices;
        private readonly ITokenGenerator _tokenGenerator;
        public LoginCommandHandler(IUserRepository repository,
            IUserServices userServices, ITokenGenerator tokenGenerator) 
        {
            _repository = repository;
            _userServices = userServices;
            _tokenGenerator = tokenGenerator;

        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.Email);
            if (user == null) 
                throw new AuthenticationException("Email or Password Incorrect");
            if (user.NeedsPasswordReset) 
                throw new AuthenticationException("Password change required");
            if (_userServices.Login(user, request.Password) == PasswordVerificationResult.Failed) 
                throw new AuthenticationException("Email or Password Incorrect");
            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshToken = await _tokenGenerator.GenerateRefreshToken(user);
            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,

            };

        }
    }
}
