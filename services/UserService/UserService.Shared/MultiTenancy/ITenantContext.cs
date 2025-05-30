

namespace UserService.Shared.MultiTenancy
{
    public interface ITenantContext
    {
        string? TenantId { get; set; }
        string? Name { get; set; }
    }
}
