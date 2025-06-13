using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos
{
    public class NewPasswordDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
    }
}