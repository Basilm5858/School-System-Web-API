using Microsoft.EntityFrameworkCore;
using School_System.DTOs;
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


        public async Task<ICollection<Subject>> GetSubjectsWithTeachers()
        {
            return await _context.Subjects
                .Include(x => x.Teacher)
                .ToListAsync();
        }
        public async Task<bool> EndPoint11(int subid1, int subid2, int subid3)
        {
            return await _context.Subjects
                .AllAsync(x => x.Id == subid1 && x.Id == subid2 && x.Id == subid3);
        }

        public async Task<ICollection<SubjectDTOEndPoint13>> EndPoint13(int teacherid)
        {
            return await _context.Subjects
                .Where(x => x.TeacherId == teacherid)
                .Select(x => new SubjectDTOEndPoint13
                {
                    Id = x.Id,
                    MaxGrade = x.MaxGrade,
                    Name = x.Name,
                }).ToListAsync();
        }

        public async Task<ICollection<Subject>> EndPoint14(int departmentId)
        {
            return await _context.Departments
                .Where(d => d.Id == departmentId)
                .SelectMany(d => d.Teachers)  
                .SelectMany(t => t.Subjects)  
                .Distinct()                   
                .ToListAsync();
        }
        public async Task<ICollection<Subject>> EndPoint16()
        {
            return await _context.Subjects
                .OrderByDescending(x => x.MaxGrade)
                .ToListAsync();
        }
    }
}
