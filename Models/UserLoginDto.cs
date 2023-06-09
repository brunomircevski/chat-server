using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class UserLoginDto
    {
        [MaxLength(32, ErrorMessage = "Username must be 4-32 characters long.")]
        [MinLength(4, ErrorMessage = "Username must be 4-32 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Letters and Numbers allowed in username.")]
        public required string username { get; set; }

        [MaxLength(4096, ErrorMessage = "Signature must be under 4096 characters long.")]
        public required string signature { get; set; }
    }
}
