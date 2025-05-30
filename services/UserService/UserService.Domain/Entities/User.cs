using UserService.Domain.Common;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Entities
{
    public class User : BaseEntity, IMultiTenant
    {
        public string? TenantId { get; set ; }
        public required string Email { get; set ; }
        public string? PasswordHash { get; set ; }
        public string? Name { get; set ; }
        public string? Surname { get; set ; }
        public string? Phone { get; set ; }
        public bool IsActive { get; set; } = false;
        public bool NeedsPasswordReset { get; set; } = true;
        public List<Role> Roles { get; set ; } = new List<Role>();

    }
}
