using School_System.DTOs;
using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IClassRoomRepo
    {
        Task<List<ClassRoom>> GetAll();
        Task<ClassRoom?> GetById(int id);
        Task<bool> NameExistsAsync(string name);
        Task Add(ClassRoom classRoom);
        void Update(ClassRoom classRoom);
        void Delete(ClassRoom classRoom);
        Task SaveChangesAsync();
    }
}
