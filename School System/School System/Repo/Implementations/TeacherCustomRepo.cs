using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Teacher>> GetTeachersWithDepartment()
        {
            return await _context.Teachers
                .Include(x => x.Department)
                .ToListAsync();
        }
    }
}
