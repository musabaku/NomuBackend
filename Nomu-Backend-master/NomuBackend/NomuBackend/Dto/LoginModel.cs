using System.ComponentModel.DataAnnotations;

namespace NomuBackend.Dto
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
    }
}

