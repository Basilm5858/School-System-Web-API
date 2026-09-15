using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_System.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        [ForeignKey(nameof(Student))]
        public int SubjectId { get; set; }
        [ForeignKey(nameof(Subject))]
        public int StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [Range(0, 100)]
        public decimal Grade { get; set; }

    }
}

//Id int Primary key
//StudentId int Required foreign key
//SubjectId int Required foreign key
//EnrollmentDate DateTime Required
//Grade decimal Value between 0 and 100