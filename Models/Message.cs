using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class Message
    {
        [Required]
        [MaxLength(64)]
        [Key]
        public string uuid { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        [MaxLength(131072, ErrorMessage = "Message content must be under 131072 characters long.")]
        public required string content { get; set; }   

        [Required]
        public Channel channel { get; set; }

        public DateTime dateCreated { get; set; } = DateTime.Now;

    }
}
