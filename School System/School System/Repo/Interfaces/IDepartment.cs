using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IDepartment : IGenericRepo<Department>
    {
        Task<Department> SearchByTeacherName(string fullName);
    }
}