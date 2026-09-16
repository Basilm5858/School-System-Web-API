using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly My_AppContext _context;
        public DepartmentController()
        {
            _context = new My_AppContext();
        }
        [HttpGet]
        public async Task<ICollection<Department>> GetDepartments()
        {
            var res = await _context.Departments.ToListAsync();
            return res;
        }

        [HttpGet("Get By Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _context.Departments.Include(x => x.Teachers)
                .FirstOrDefaultAsync();

            if(res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }
    }
}
