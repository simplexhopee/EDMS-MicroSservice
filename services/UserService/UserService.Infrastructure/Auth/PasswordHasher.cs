using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public Task<string> Hash(string password)
        {
            throw new NotImplementedException();
        }
    }
}
