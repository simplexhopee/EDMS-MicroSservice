using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Shared.MultiTenancy
{
    public class TenantContext : ITenantContext
    {
        public string? TenantId { get; set; }

        public string? Name { get; set; }
    }
}
