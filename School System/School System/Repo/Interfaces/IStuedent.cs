using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IStuedent : IGenericRepo<Student>
    {
        Task<List<Student>> IncludeClassRoom();

    }
}
