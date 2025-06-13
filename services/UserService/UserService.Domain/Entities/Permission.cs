
using UserService.Shared.Common;

namespace UserService.Domain.Entities
{
    public class Permission : RootEntity, IMultiTenant
    {
        public string? TenantId { get; set; }
        public required string Permissions { get; set; }
    }
}