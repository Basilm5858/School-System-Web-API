using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class SubjectCustomRepo : GenericRepo<Subject> , ISubject
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
