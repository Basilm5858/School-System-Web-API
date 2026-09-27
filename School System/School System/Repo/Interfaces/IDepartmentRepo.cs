using School_System.Models;

namespace School_System.Repo.Interfaces
{
    public interface IDepartmentRepo
    {
        Task<List<Department>> GetAll();
        Task<Department?> GetById(int id);
        Task<Department?> SearchByTeacherName(string fullName);
        Task<bool> NameExistsAsync(string name);
        Task AddAsync(Department department);

        void Update(Department department);

        void Delete(Department department);
        Task SaveChangesAsync();
    }
}
