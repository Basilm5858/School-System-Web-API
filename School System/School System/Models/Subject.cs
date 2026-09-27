using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_System.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(1,100)]
        public int MaxGrade { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}