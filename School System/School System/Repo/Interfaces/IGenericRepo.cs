using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);
        Task AddAsync(T Entity);
        void Delete(T Entity);
        Task SaveChangesAsync();
    }
}
