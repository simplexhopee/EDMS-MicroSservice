﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Interfaces
{
    public interface IPasswordHasher
    {
       Task<string> Hash(string password);
    }
}
