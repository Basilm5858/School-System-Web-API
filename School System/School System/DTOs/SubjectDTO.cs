using System.ComponentModel.DataAnnotations;

namespace School_System.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(1, 100)]
        public int MaxGrade { get; set; }
        public string TeacherName { get; set; } = string.Empty;
    }
    public class CreateSubjectDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(1, 100)]
        public int MaxGrade { get; set; }
        public int TeacherId { get; set; }


    }
    public class UpdateSubjectDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(1, 100)]
        public int MaxGrade { get; set; }
        public int TeacherId { get; set; }
    }
}
