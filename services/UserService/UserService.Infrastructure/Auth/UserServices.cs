using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repositories;
using UserService.Shared.Database;

namespace UserService.Infrastructure.Auth
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly PasswordHasher<User> _hasher;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(UserManager<User> userManager, IPasswordGenerator passwordGenerator, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _hasher = new PasswordHasher<User>();
            _passwordGenerator = passwordGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task ResetPassword(User user, string password, string token)
        {
            await _userManager.ResetPasswordAsync(user, token, password);
            user.NeedsPasswordReset = false;
            user.EmailConfirmed = true;
            _unitOfWork.Repository<User, Guid>()
                .Update(user);
             await _unitOfWork.CompleteAsync();
        }

        public async Task<string> GeneratePasswordResetToken(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public async Task NewUser(User user)
        {
            var password = _passwordGenerator.GenerateRandomPassword();
         await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> VerifyPasswordResetToken(User user, string token)
        {
            return await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        }

        public PasswordVerificationResult Login(User user, string password)
        {
          return  _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }
    }
}
