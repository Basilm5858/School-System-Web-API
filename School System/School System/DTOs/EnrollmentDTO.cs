using System.ComponentModel.DataAnnotations;

namespace School_System.DTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        [Range(0, 100)]
        public decimal Grade { get; set; }

    }
    public class CreateEnrollmentDTO
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        [Range(0, 100)]
        public decimal Grade { get; set; }
    }
    public class UpdateEnrollmentDTO
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        [Range(0, 100)]
        public decimal Grade { get; set; }
    }
}
