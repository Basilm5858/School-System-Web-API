using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface ITeacher : IGenericRepo<Teacher>
    {
        Task<List<Teacher>> GetTeachersWithDepartment();
    }
}
