using System.ComponentModel.DataAnnotations;

namespace School_System.DTOs
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
    public class ResponseDTO
    {
        public string token { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
    }

}
