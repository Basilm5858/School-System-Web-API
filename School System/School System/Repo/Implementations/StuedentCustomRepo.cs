using Microsoft.EntityFrameworkCore;
using School_System.Models;

namespace School_System.Repo.Implementations
{
    public class StuedentCustomRepo : GenericRepo<Student>
    {
        private readonly My_AppContext _context;
        public StuedentCustomRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Student>> IncludeClassRoom()
        {
            return await _context.Students
                .Include(x => x.ClassRoom)
                .ToListAsync();
        }
    }
}
