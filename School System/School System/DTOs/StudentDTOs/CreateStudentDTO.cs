namespace School_System.DTOs.StudentDTOs
{
    public class CreateStudentDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ClassRoomId { get; set; }

    }
}
