using System.ComponentModel.DataAnnotations;

namespace School_System.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
    public class CreateDepartmentDTO
    {
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty; 
    }
    public class UpdateDepartmentDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
