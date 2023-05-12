using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class InviteDto
    {
        [MaxLength(32, ErrorMessage = "Username must be 4-32 characters long.")]
        [MinLength(4, ErrorMessage = "Username must be 4-32 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Letters and Numbers allowed in username.")]
        public required string username { get; set; }

        [MaxLength(4096, ErrorMessage = "Invite content must be under 4096 characters long.")]
        public required string content { get; set; }

        [MaxLength(1024, ErrorMessage = "Invite encrypted key must be under 1024 characters long.")]
        public required string encryptedKey { get; set; }
    }
}
