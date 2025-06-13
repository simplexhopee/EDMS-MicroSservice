using UserService.Shared.Common;

namespace UserService.Domain.Entities
{
    public class RefreshToken : RootEntity, IMultiTenant
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(7);
        public string UserId { get; set; }
        public string? TenantId { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime? TimeRevoked { get; set; }
    }
}
