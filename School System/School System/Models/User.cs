using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace School_System.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
