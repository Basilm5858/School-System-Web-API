namespace School_System.DTOs.StudentDTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ClassRoomName { get; set; } = string.Empty;
    }
}
