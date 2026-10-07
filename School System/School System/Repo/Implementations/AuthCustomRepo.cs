using Microsoft.EntityFrameworkCore;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class AuthCustomRepo : GenericRepo<User>, IAuth
    {
        private readonly My_AppContext _context;
        public AuthCustomRepo(My_AppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
