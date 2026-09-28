using Microsoft.EntityFrameworkCore;
using School_System.Models;

namespace School_System.Repo.Implementations
{
    public class SubjectCustomRepo : GenericRepo<Subject>
    {
        private readonly My_AppContext _context;
        public SubjectCustomRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Subject>> GetSubjectsWithTeachers()
        {
            return await _context.Subjects
                .Include(x => x.Teacher)
                .ToListAsync();
        }

    }
}
