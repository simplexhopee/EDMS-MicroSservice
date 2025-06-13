

using UserService.Shared.Common;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace UserService.Domain.Entities
{
    public class User : IdentityUser, IAuditable , IMultiTenant
    {
        public string? TenantId { get; set ; }
        public string? Name { get; set ; }
        public string? Surname { get; set ; }
        public bool IsActive { get; set; } = false;
        public bool NeedsPasswordReset { get; set; } = true;
        public List<Role> Roles { get; set ; } = new List<Role>();
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
