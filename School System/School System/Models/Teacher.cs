using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace School_System.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required, Range(0, int.MaxValue) ]
        public int Salary { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}

//Id int Primary key
//FirstName string Required, maximum length 50
//LastName string Required, maximum length 50
//Email string Required, valid email address, maximum

//length 150

//PhoneNumber string Optional (nullable), maximum length 20,
//valid phone number if provided
//Salary decimal Required, must be greater than or equal to 0