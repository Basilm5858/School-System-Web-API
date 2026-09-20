using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.DepartmentDTO.DepartmentDTOs;
using School_System.DTOs.DepartmentDTOs;
using School_System.Models;
/////////////
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
            var departments = await _context.Departments
                .Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToListAsync();

            return Ok(departments);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetDepartments()
        //{
        //    var departments = await _context.Departments.ToListAsync();

        //    var result = new List<DepartmentDTO>();

        //    foreach (var department in departments)
        //    {
        //        var dto = new DepartmentDTO
        //        {
        //            Name = department.Name,
        //            Description = department.Description
        //        };

        //        result.Add(dto);
        //    }

        //    return Ok(result);
        //}
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
                .Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (res == null)
            {
                return NotFound("Department Not Found");
            }

            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDTO dto)
        {
            var dept = await _context.Departments.FindAsync(id);

            if (dept == null)
            {
                return NotFound("Department Not Found");
            }

            dept.Name = dto.Name;
            dept.Description = dto.Description;

            await _context.SaveChangesAsync();

            return  NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
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
