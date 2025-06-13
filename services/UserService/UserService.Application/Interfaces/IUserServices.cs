using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserServices
    {
        Task ResetPassword(User user, string password, string token);
        Task<string> GeneratePasswordResetToken(User user);
        string HashPassword(User user, string password);
        Task NewUser(User user);
        Task<bool> VerifyPasswordResetToken(User user, string token);
        PasswordVerificationResult Login(User user, string password);
    }
}