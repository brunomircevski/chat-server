using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class UserLoginDto
    {
        [MaxLength(30, ErrorMessage = "Username must be 4-30 characters long")]
        [MinLength(4, ErrorMessage = "Username must be 4-30 characters long")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Letters and Numbers allowed in username.")]
        public required string username { get; set; }

        [MaxLength(64, ErrorMessage = "Answer must be 64 characters long")]
        [MinLength(64, ErrorMessage = "Answer must be 64 characters long")]
        public required string answer { get; set; }
    }
}
