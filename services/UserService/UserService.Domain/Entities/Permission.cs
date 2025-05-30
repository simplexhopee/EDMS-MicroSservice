using UserService.Domain.Common;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Entities
{
    public class Permission : RootEntity, IMultiTenant
    {
        public string? TenantId { get; set; }
        public required string Permissions { get; set; }
    }
}