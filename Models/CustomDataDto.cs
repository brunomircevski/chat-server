using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class CustomDataDto
    {
        [MaxLength(131072, ErrorMessage = "Data must be under 131072 characters long.")]
        public required string data { get; set; }

    }
}
