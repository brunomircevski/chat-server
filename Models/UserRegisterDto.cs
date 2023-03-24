using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class UserRegisterDto
    {
        [MaxLength(32, ErrorMessage = "Username must be 4-32 characters long.")]
        [MinLength(4, ErrorMessage = "Username must be 4-32 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Letters and Numbers allowed in username.")]
        public required string username { get; set; }

        [MaxLength(128, ErrorMessage = "Password must be 8-128 characters long.")]
        [MinLength(8, ErrorMessage = "Password must be 8-128 characters long.")]
        public required string password { get; set; }

        [MaxLength(4096, ErrorMessage = "Public key must be under 4096 characters long.")]
        public required string publicKey { get; set; }

        [MaxLength(4096, ErrorMessage = "Private key must be under 4096 characters long.")]
        public required string encryptedPrivateKey { get; set; }
    }
}
