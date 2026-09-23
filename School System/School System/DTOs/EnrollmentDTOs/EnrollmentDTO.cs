namespace School_System.DTOs.EnrollmentDTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public decimal Grade { get; set; }

    }
}
