namespace School_System.DTOs.ClassRoomDTOs
{
    public class CreateClassRoomsDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int GradeLevel { get; set; }
    }
}
