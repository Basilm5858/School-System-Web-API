using System.ComponentModel.DataAnnotations;

namespace School_System.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string ClassRoomName { get; set; } = string.Empty;
    }
    public class UpdateStudentDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public int ClassRoomId { get; set; }
    }
    public class CreateStudentDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public int ClassRoomId { get; set; }

    }
}
