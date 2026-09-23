namespace School_System.DTOs.SubjectDTOs
{
    public class CreateSubjectDTO
    {
        public string Name { get; set; } = string.Empty;
        public int MaxGrade { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TeacherId { get; set; }


    }
}
