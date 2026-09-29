using School_System.DTOs;
using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface ISubject : IGenericRepo<Subject>
    {
        Task<ICollection<Subject>> GetSubjectsWithTeachers();
        Task<Subject> EndPoint2Async(int id);
        Task<Subject> EndPoint7(int teacherid);
        Task<bool> EndPoint11(int subid1, int subid2, int subid3);
        Task<ICollection<SubjectDTOEndPoint13>> EndPoint13(int teacherid);
        Task<ICollection<Subject>> EndPoint14(int departmentid);
        Task<ICollection<Subject>> EndPoint16();



    }
}
