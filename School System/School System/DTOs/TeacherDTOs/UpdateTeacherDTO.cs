namespace School_System.DTOs.TeacherDTOs
{
    public class UpdateTeacherDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
    }
}
