using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly My_AppContext _context;

        public DepartmentRepo(My_AppContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Departments
                .ToListAsync();
        }

        public async Task<Department?> GetById(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Department?> SearchByTeacherName(string fullName)
        {
            return await _context.Departments
                .Include(x => x.Teachers)
                .FirstOrDefaultAsync(x =>
                    x.Teachers.Any(t =>
                        (t.FirstName + " " + t.LastName)
                        .Contains(fullName)));
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Departments
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
