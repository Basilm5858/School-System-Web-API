using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class DepartmentRepo : GenericRepo<Department> , IDepartment
    {
        private readonly My_AppContext _context;

        public DepartmentRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Department> SearchByTeacherName(string fullName)
        {
            return await _context.Departments
                .Include(x => x.Teachers)
                .FirstOrDefaultAsync(x =>
                    x.Teachers.Any(t =>
                        (t.FirstName + " " + t.LastName)
                        .Contains(fullName)));
        }
    }
}
