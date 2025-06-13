using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Shared.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using UserService.Shared.Database;


namespace UserService.Infrastructure.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private IRefreshTokenRepository _repository;

        public TokenGenerator(IOptions<JwtSettings> jwtSettings, 
            IUnitOfWork unitOfWork, IRefreshTokenRepository repository)
        {
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public string GenerateAccessToken(User user)
        {
            var roles = user.Roles.Select(r => r.Name).ToList();
            var permissions = user.Roles.SelectMany(r => r.Permissions)
                .Select(p => p.Permissions).ToList();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name, user.Name!.ToString()),
                new Claim("Organization", user.TenantId ?? "SuperAdmin"),


            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            claims.AddRange(permissions.Select(p => new Claim("Permissions", p)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var token = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                TenantId = user.TenantId,
            };
            await _unitOfWork.Repository<RefreshToken, Guid>()
                .AddAsync(token);
            await _unitOfWork.CompleteAsync();
            return token.Token;
              
        }


    }
}
