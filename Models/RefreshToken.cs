using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }

        public User User { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
    }
}