using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class User
    {
        [MaxLength(64)]
        public string uuid { get; set; } = String.Empty;

        [MaxLength(30)]
        public string username { get; set; } = String.Empty;
        
        [MaxLength(256)]
        [MinLength(64)]
        public string challenge { get; set; } = String.Empty;

        [MaxLength(64)]
        [MinLength(64)]
        public string answer { get; set; } = String.Empty;

        [MaxLength(4096)]
        public string publicKey { get; set; } = String.Empty;

        [MaxLength(4096)]
        public string encryptedPrivateKey { get; set; } = String.Empty;

        [MaxLength(131072)]
        public string encryptedUserData { get; set; } = String.Empty;

        public DateTime lastLogin { get; set; } = DateTime.Today;

        [Range(1,365)]
        public int tokenExpireDays { get; set; } = 30;

        public User(string uuid, string username, string challenge, string answer, string publicKey, string encryptedPrivateKey)
        {
            this.uuid = uuid;
            this.username = username;
            this.challenge = challenge;
            this.answer = answer;
            this.publicKey = publicKey;
            this.encryptedPrivateKey = encryptedPrivateKey;
        }
    }
}
//Klucz aes 256 generowany z username+password, nie trafia na serwer