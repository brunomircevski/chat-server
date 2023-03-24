using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class User
    {
        [Required]
        [MaxLength(64)]
        public string uuid { get; set; } = String.Empty;

        [Required]
        [MaxLength(32)]
        [MinLength(4)]
        public string username { get; set; } = String.Empty;
        
        [Required]
        [MaxLength(60)]
        [MinLength(60)]
        public string passwordHash { get; set; }

        [Required]
        [MaxLength(4096)]
        public string publicKey { get; set; } = String.Empty;

        [Required]
        [MaxLength(4096)]
        public string encryptedPrivateKey { get; set; } = String.Empty;

        [Required]
        [MaxLength(131072)]
        public string encryptedUserData { get; set; } = String.Empty;

        public DateTime lastLogin { get; set; } = DateTime.Today;

        [Required]
        [Range(1,365)]
        public int tokenExpireDays { get; set; } = 30;

        public User(string uuid, string username, string passwordHash, string publicKey, string encryptedPrivateKey)
        {
            this.uuid = uuid;
            this.username = username;
            this.passwordHash = passwordHash;
            this.publicKey = publicKey;
            this.encryptedPrivateKey = encryptedPrivateKey;
        }
    }
}
