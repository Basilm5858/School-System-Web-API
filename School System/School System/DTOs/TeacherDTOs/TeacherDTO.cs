using School_System.Models;

namespace School_System.DTOs.TeacherDTOs
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public int Salary { get; set; }
    }
}
