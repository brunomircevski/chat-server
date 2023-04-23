using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class User
    {
        [Required]
        [MaxLength(64)]
        [Key]
        public string uuid { get; set; } = String.Empty;

        [Required]
        [MaxLength(32)]
        [MinLength(4)]
        public string username { get; set; } = String.Empty;

        [Required]
        [MaxLength(4096)]
        public string publicKey { get; set; } = String.Empty;

        [Required]
        [MaxLength(131072)]
        public string encryptedUserData { get; set; } = String.Empty;

        //public DateTime lastLogin { get; set; } = DateTime.Today;

        public User(string uuid, string username, string publicKey)
        {
            this.uuid = uuid;
            this.username = username;
            this.publicKey = publicKey;
        }
    }
}
