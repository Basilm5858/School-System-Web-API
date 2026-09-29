using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class ClassRoomCustomRepo : GenericRepo<ClassRoom>, IClassRoom
    {
        private readonly My_AppContext _context;
        public ClassRoomCustomRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClassRoom> EndPoint3(int capacity)
        {
            return await _context.Classrooms
                .FirstOrDefaultAsync(x => x.Capacity == capacity);
        }
        public async Task<ClassRoom> EndPoint5(string name)
        {
            return await _context.Classrooms
                .SingleOrDefaultAsync(x => x.Name == name);
        }
        public async Task<ClassRoom> EndPoint8(int index)
        {
            return await _context.Classrooms
                .OrderBy(x => x.Id)
                .ElementAtAsync(index);
        }
    }
}
