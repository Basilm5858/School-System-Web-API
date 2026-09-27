using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class ClassRoomRepo : IClassRoomRepo
    {
        private readonly My_AppContext _context;
        public ClassRoomRepo(My_AppContext context)
        {
            _context = context;
        }

        public async Task<List<ClassRoom>> GetAll()
        {
            return await _context.Classrooms.ToListAsync();
        }
        public async Task<ClassRoom?> GetById(int id)
        {
            return await _context.Classrooms
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Classrooms.AnyAsync(r => r.Name == name);
        }
        public async Task Add(ClassRoom classRoom)
        {
            await _context.Classrooms.AddAsync(classRoom);
        }
        public void Update(ClassRoom classRoom)
        {
            _context .Update(classRoom);
        }
        public void Delete(ClassRoom classRoom)
        {
            _context.Remove(classRoom);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
