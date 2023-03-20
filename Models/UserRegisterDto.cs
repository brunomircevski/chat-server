using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class UserRegisterDto
    {
        [MaxLength(30, ErrorMessage = "Username must be 4-30 characters long")]
        [MinLength(4, ErrorMessage = "Username must be 4-30 characters long")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Letters and Numbers allowed in username.")]
        public required string username { get; set; }

        [MaxLength(256, ErrorMessage = "Challenge must be 64-256 characters long")]
        [MinLength(64, ErrorMessage = "Challenge must be 64-256 characters long")]
        public required string challenge { get; set; }

        [MaxLength(64, ErrorMessage = "Answer must be 64 characters long")]
        [MinLength(64, ErrorMessage = "Answer must be 64 characters long")]
        public required string answer { get; set; }

        [MaxLength(4096, ErrorMessage = "Public key must be under 4096 characters long")]
        public required string publicKey { get; set; }

        [MaxLength(4096, ErrorMessage = "Private key must be under 4096 characters long")]
        public required string encryptedPrivateKey { get; set; }
    }
}
