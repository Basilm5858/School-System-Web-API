using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface ISubject : IGenericRepo<Subject>
    {
        Task<List<Subject>> GetSubjectsWithTeachers();
        Task<Subject> EndPoint2Async(int id);
        Task<Subject> EndPoint7(int teacherid);


    }
}
