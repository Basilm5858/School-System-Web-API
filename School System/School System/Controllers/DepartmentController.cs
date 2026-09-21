using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.DepartmentDTO.DepartmentDTOs;
using School_System.DTOs.DepartmentDTOs;
using School_System.Mapping;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly My_AppContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();

            var result = _mapper.Map<List<DepartmentDTO>>(departments);

            return Ok(result);
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

            var result = _mapper.Map<DepartmentDTO>(department);

            return Ok(result);
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

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO dto)
        {
            var department = _mapper.Map<Department>(dto);

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDTO dto)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            _mapper.Map(dto, department);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _context.Departments
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
