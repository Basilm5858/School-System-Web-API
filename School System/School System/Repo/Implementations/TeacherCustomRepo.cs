using Microsoft.EntityFrameworkCore;
using School_System.DTOs;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class TeacherCustomRepo : GenericRepo<Teacher>, ITeacher
    {
        private readonly My_AppContext _context;
        public TeacherCustomRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Teacher>> GetTeachersWithDepartment()
        {
            return await _context.Teachers
                .Include(x => x.Department)
                .ToListAsync();
        }
        public async Task<ICollection<Teacher>> EndPoint1Async(int id, decimal salary)
        {
            return await _context.Teachers
                .Where(x => x.Id == id && x.Salary >= salary)
                .ToListAsync();
        }
        public async Task<Teacher> EndPoint4Async(string email)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> EndPoint9Async(int id)
        {
            return await _context.Teachers
                .AnyAsync(x => x.Id == id && x.Subjects.Count != 0);
                
        }
        public async Task<ICollection<TeacherDTOEndPoint12>> EndPoint12Async(int departmentid)
        {
            return await _context.Teachers
                .Where(x => x.DepartmentId == departmentid)
                .Select(x => new TeacherDTOEndPoint12
                {
                    FullName = x.FirstName + " " + x.LastName,
                    Id = x.Id,
                }).ToListAsync();
        }
        public async Task<ICollection<Teacher>> EndPoint15Async()
        {
            return await _context.Teachers
                .OrderBy(x => x.Salary)
                .ToListAsync();
        }
    }
}
