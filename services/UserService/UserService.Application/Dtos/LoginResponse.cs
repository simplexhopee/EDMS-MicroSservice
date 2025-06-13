using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Dtos
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessExpiry { get; set; } = DateTime.Now.AddHours(4);
        public DateTime RefreshExpiry { get; set; } = DateTime.Now.AddDays(7);
    }
}
