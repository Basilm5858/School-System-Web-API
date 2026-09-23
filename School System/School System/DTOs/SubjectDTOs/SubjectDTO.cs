namespace School_System.DTOs.SubjectDTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public int MaxGrade { get; set; }

    }
}
