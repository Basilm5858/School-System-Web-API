namespace School_System.DTOs.SubjectDTOs
{
    public class UpdateSubjectDTO
    {
        public string Name { get; set; } = string.Empty;
        public int MaxGrade { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
