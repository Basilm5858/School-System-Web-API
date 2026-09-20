using School_System.Models;

namespace School_System.DTOs.TeacherDTOs
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }

    }
}
