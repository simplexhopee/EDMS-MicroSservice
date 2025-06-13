using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
    }
}