using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UserService.Shared.Configuration;

namespace UserService.API.Extensions
{
    public static class JwtAuthExtension
    {
        
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
           
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey ?? "")),
                     ValidIssuer = jwtSettings.ValidIssuer,
                     ValidAudience = jwtSettings.ValidAudience
                 };
             });
            return services;
        }


    }
}
