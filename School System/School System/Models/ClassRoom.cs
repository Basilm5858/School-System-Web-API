using System.ComponentModel.DataAnnotations;

namespace School_System.Models
{
    public class ClassRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, Range(1, 12)]
        public int GradeLevel { get; set; }
        [Required, Range(1, 100)]
        public int Capacity { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
