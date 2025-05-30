﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;


namespace UserService.Application.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<bool> ExistsByEmailAndTenant(string email);
        
    }
}
