using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IAuth : IGenericRepo<User>
    {
        Task<User?> GetUserByUserName(string userName);
    }
}
