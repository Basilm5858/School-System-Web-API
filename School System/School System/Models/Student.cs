using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace School_System.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public ClassRoom ClassRoom { get; set; }
        [ForeignKey(nameof(ClassRoom))]
        public int ClassRoomId { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
