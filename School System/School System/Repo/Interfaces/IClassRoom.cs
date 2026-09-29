using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IClassRoom:IGenericRepo<ClassRoom>
    {
        Task<ClassRoom> EndPoint3(int capacity);
        Task<ClassRoom> EndPoint5(string name);
        Task<ClassRoom> EndPoint8(int index);
    }
}
