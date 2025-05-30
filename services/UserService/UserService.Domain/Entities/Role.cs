using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Entities
{
    public class Role : RootEntity, IMultiTenant
    {
        public string? TenantId { get; set; }
        public required string RoleName { get; set; }
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        
    }
}
