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

        public async Task<Subject> EndPoint2Async(int id)
        {
            return await _context.Subjects
                .Include(x => x.Teacher)
                .FirstAsync(x => x.TeacherId == id);
        }
        public async Task<Subject> EndPoint7(int teacherid)
        {
            return await _context.Subjects
                .Include(x => x.Teacher)
                .OrderByDescending(x => x.Id)
                .LastOrDefaultAsync(x => x.TeacherId == teacherid);
        }


        public async Task<List<Subject>> GetSubjectsWithTeachers()
        {
            return await _context.Subjects
                .Include(x => x.Teacher)
                .ToListAsync();
        }

    }
}
