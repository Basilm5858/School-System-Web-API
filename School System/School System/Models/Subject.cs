using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_System.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required, Range(1,100)]
        public int MaxGrade { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; } = new List<Enrollment>();

    }
}

//Id int Primary key
//Name string Required, maximum length 100
//Description string Optional (nullable), maximum length 500
//MaxGrade int Required, value between 1 and 100