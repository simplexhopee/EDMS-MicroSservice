

namespace UserService.Shared.Configuration
{
    public class JwtSettings
    {
        public string? IssuerSigningKey { get; set; }
        public string? ValidIssuer {  get; set; }
        public string? ValidAudience { get; set; }
    }
}
