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
        public async Task<IActionResult> GetDepartments()
        {
            var res = await _context.Departments.Include(x => x.Teachers).ToListAsync();
            return Ok(res);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchByTeacherName(string fullName)
        {
            var department = await _context.Departments
                .Include(d => d.Teachers)
                .FirstOrDefaultAsync(d =>
                    d.Teachers.Any(t =>
                        (t.FirstName + " " + t.LastName).Contains(fullName)));

            if (department == null)
            {
                return NotFound("Teacher Not Found");
            }

            return Ok(department);
        }

        // GET: api/Department/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _context.Departments
                .Include(x => x.Teachers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (res == null)
            {
                return NotFound("Department Not Found");
            }

            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department)
        {
            var existingDepartment = await _context.Departments
                .Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == id);

            if (existingDepartment == null)
            {
                return NotFound("Department Not Found");
            }
            
            existingDepartment.Name = department.Name;
            existingDepartment.Description = department.Description;
            await _context.SaveChangesAsync();

            return  NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest("The Department cannot be null");
            }
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, Department department)
        {
            var res = await _context.Departments
                .Include(x => x.Teachers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (res == null)
            {
                return BadRequest("Department Not Found");
            }

            _context.Departments.Remove(res);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
