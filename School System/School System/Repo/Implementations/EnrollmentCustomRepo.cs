using Microsoft.EntityFrameworkCore;
using School_System.Models;

namespace School_System.Repo.Implementations
{
    public class EnrollmentCustomRepo : GenericRepo<Enrollment>
    {
        private readonly My_AppContext _context;
        public EnrollmentCustomRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Enrollment>> GetEnrollmentsWithStudentAndSubject()
        {
            return await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .ToListAsync();
        }
    }
}
