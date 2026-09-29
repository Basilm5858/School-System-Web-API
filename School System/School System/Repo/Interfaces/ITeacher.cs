using School_System.DTOs;
using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface ITeacher : IGenericRepo<Teacher>
    {
        Task<ICollection<Teacher>> GetTeachersWithDepartment();
        Task<ICollection<Teacher>> EndPoint1Async(int id, decimal salary);
        Task<Teacher> EndPoint4Async(string email);
        Task<bool> EndPoint9Async(int id);
        Task<ICollection<TeacherDTOEndPoint12>> EndPoint12Async(int departmentid);
        Task<ICollection<Teacher>> EndPoint15Async();
        

    }
}
