using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IEnrollment : IGenericRepo<Enrollment>
    {
        Task<List<Enrollment>> GetEnrollmentsWithStudentAndSubject();
        Task<Enrollment> EndPoint6(int subjectid);
    }
}
