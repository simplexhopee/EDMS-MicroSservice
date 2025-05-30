using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Interfaces;

namespace UserService.Shared.Auth
{
    public class CurrentUser : ICurrentUser
    {
       public string? Email { get; private set; } 
        public List<string> Roles { get; private set; } = new List<string>();
       public List<string> Permissions { get; private set; } = new List<string>();

        public void SetUser(string email, List<string> roles, List<string> permissions)
        {
            if (!string.IsNullOrEmpty(Email)) throw new InvalidOperationException("Current User is set already");
            Email = email;
            Roles = roles;
            Permissions = permissions;
            
        }

    }
}
