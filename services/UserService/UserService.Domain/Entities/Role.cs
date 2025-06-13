

using Microsoft.AspNetCore.Identity;
using UserService.Shared.Common;

namespace UserService.Domain.Entities
{
    public class Role : IdentityRole, IAuditable, IMultiTenant
    {
        public string? TenantId { get; set; }
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }

    }
}
