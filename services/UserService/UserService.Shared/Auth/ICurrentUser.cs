using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Shared.Auth
{
    public interface ICurrentUser
    {
        string Email { get; }
        List<string> Roles { get;  }
        List<string> Permissions { get;  }

        void SetUser(string email, List<string> roles, List<string> permissions);

    }
}
