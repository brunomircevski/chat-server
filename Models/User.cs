using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class User
    {
        public User(string uuid, string username, string publicKey)
        {
            this.uuid = uuid;
            this.username = username;
            this.publicKey = publicKey;
            this.acceptsInvites = true;
        }

        [Required]
        [MaxLength(64)]
        [Key]
        public string uuid { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(4)]
        public string username { get; set; }

        [Required]
        [MaxLength(4096)]
        public string publicKey { get; set; }

        [MaxLength(131072)]
        public string encryptedUserData { get; set; }

        [MaxLength(131072)]
        public string encryptedInvitesData { get; set; }

        [Required]
        public bool acceptsInvites { get; set; }

        public List<Invite> Invites { get; set; }
    }
}
