

namespace UserService.Shared.Common
{
    public interface IMultiTenant
    {
        string? TenantId { get; set; }
    }
}
