namespace School_System.DTOs.TeacherDTOs
{
    public class CreateTeacherDTO
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Salary { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
