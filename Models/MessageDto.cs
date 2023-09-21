using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class MessageDto
    {
        [Required]
        [MaxLength(131072, ErrorMessage = "Message content must be under 131072 characters long.")]
        public required string content { get; set; }   

        [Required]
        [MaxLength(256, ErrorMessage = "Channel access key must be under 256 characters long.")]
        public required string channelAccessKey { get; set; }

    }

    public class MessageGetDto
    {
        public string uuid { get; set; } 
        public required string content { get; set; }   
        public DateTime date { get; set; }
    }
}
