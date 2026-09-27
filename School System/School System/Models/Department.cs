using System.ComponentModel.DataAnnotations;

namespace School_System.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
