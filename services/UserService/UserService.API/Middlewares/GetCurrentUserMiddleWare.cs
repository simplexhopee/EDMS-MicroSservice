using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Shared.Auth;

namespace UserService.API.Middlewares
{
    public class GetCurrentUserMiddleWare
    {
        private readonly RequestDelegate _next;
        

        public GetCurrentUserMiddleWare(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context, ICurrentUser _currentUser)
        {
            var user = context.User;
            var roles = user.Claims
            .Where(c => c.Type == "roles")
            .Select(c => c.Value)
            .ToList();

            var permissions = user.Claims
           .Where(c => c.Type == "permissions")
           .Select(c => c.Value)
           .ToList();

            if (user.FindFirst("email") != null) _currentUser.SetUser(user?.FindFirst("email").Value, roles, permissions);
            await  _next(context);
        }
    }
}
