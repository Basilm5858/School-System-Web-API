using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface ITeacher : IGenericRepo<Teacher>
    {
        Task<List<Teacher>> GetTeachersWithDepartment();
        Task<ICollection<Teacher>> EndPoint1Async(int id, decimal salary);
        public Task<Teacher> EndPoint4Async(string email);
        public Task<bool> EndPoint9Async(int id);

    }
}
