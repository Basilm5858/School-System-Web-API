using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.TeacherDTOs;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly My_AppContext _context;
        public TeacherController()
        {
            _context = new My_AppContext();
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _context.Teachers
                .Include(x => x.Department)
                .Select(x => new TeacherDTO
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FirstName + " " + x.LastName,
                    DepartmentName = x.Department.Name

                }).ToListAsync();

            return Ok(teachers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _context.Teachers
                .Include(x => x.Department)
                .Select(x => new TeacherDTO
                {
                    Email = x.Email,
                    Id = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    DepartmentName = x.Department.Name
                }).FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDTO dto)
        {
            var res = new Teacher
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Salary = dto.Salary,
                PhoneNumber = dto.PhoneNumber,
                DepartmentId = dto.DepartmentId
                
            };

            _context.Teachers.Add(res);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDTO dto)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.Email = dto.Email;
            teacher.DepartmentId = dto.DepartmentId;
            teacher.Salary = dto.Salary;
            teacher.PhoneNumber = dto.PhoneNumber;
            
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
