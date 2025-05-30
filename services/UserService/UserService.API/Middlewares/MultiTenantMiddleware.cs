using UserService.Shared.MultiTenancy;

namespace UserService.API.Middlewares
{
    public class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;
       
        public MultiTenantMiddleware(RequestDelegate next)
        {
            _next = next;   
            
        }
        public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
        {
            var header = context.Request.Headers["X-Tenant-ID"];
            if (!string.IsNullOrWhiteSpace(header))
            {
                tenantContext.TenantId = header;

            }
            else
            {
                var host = context.Request.Host.Host;
                if (host != null)
                {
                    string tenantId = ExtractSubdomain(host);
                    if (tenantId != "")
                    {
                        tenantContext.TenantId = tenantId;
                    }
                }
            }
            
            await _next(context);
        }

        private string ExtractSubdomain(string domain)
        {
            string[] parts = domain.Split('.');
            if (parts.Length == 3) return parts[0];
            return "";
        }

        private void ResolveTenant(string tenantId) => throw new NotImplementedException();
    }
}
