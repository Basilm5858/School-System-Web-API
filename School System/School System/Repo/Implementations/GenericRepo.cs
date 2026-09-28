using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly My_AppContext _context;
        public DbSet<T> db { get; set; }
        public GenericRepo(My_AppContext context)
        {
            _context = context;
            db = _context.Set<T>();
        }

        public async Task AddAsync(T Entity)
        {
            await db.AddAsync(Entity);
        }

        public void Delete(T Entity)
        {
            db.Remove(Entity);
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await db.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await db.FindAsync(id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
