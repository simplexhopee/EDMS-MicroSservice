

namespace UserService.Domain.Interfaces
{
    public interface IMultiTenant
    {
        string? TenantId { get; set; }
    }
}
