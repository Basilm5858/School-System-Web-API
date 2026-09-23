using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.StudentDTOs;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly My_AppContext _context;
        private readonly IMapper _mapper;

        public StudentController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var stud = await _context.Students
                .Include(x => x.ClassRoom)
                .ToListAsync();

            var res = _mapper.Map<List<StudentDTO>>(stud);

            return Ok(res);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stud = await _context.Students
                .Include(x => x.ClassRoom)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(stud == null)
            {
                return BadRequest("Student Not Found");
            }

            var res = _mapper.Map<StudentDTO>(stud);

            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDTO dto)
        {
            var student = _mapper.Map<Student>(dto);
            
            if (student == null)
            {
                return BadRequest("ClassRoom Cannot Be Null");
            }

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDTO dto)
        {
            var student = _mapper.Map<Student>(dto);

            if (student == null)
            {
                return BadRequest("ClassRoom Cannot Be Null");
            }

            _mapper.Map(dto, student);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
