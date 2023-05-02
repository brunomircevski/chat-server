using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class Invite
    {
        [Required]
        [MaxLength(64)]
        [Key]
        public string uuid { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        public User user { get; set; }

        [Required]
        [MaxLength(4096)]
        public required string content { get; set; }

        [Required]
        [MaxLength(256)]
        public required string accessKey { get; set; }
    }
}
