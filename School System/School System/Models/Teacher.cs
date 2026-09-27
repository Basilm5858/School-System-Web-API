using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace School_System.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, Range(1, int.MaxValue) ]
        public int Salary { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
