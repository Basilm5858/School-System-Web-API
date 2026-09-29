using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface ISubject : IGenericRepo<Subject>
    {
        Task<List<Subject>> GetSubjectsWithTeachers();
    }
}
