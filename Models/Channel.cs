using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class Channel
    {
        [Required]
        [MaxLength(64)]
        [Key]
        public string uuid { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        [MaxLength(256)]
        public required string accessKey { get; set; }

        public DateOnly dateCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
