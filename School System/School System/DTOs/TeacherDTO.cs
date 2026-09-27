using System.ComponentModel.DataAnnotations;
using School_System.Models;

namespace School_System.DTOs
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        [Required, Range(1, int.MaxValue)]
        public int Salary { get; set; }
    }
    public class CreateTeacherDTO
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required, Range(1, int.MaxValue)]
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
    public class UpdateTeacherDTO
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required, Range(1, int.MaxValue)]
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
