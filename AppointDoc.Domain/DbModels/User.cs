using System.ComponentModel.DataAnnotations;

namespace AppointDoc.Domain.DbModels
{
    public sealed class User
    {
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
