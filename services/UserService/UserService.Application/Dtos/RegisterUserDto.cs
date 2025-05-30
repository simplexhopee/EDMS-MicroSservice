

using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }
        [Required]
        public required string Phone { get; set; }
    }
}
