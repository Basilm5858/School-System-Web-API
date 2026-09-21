using AutoMapper;
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
        private readonly IMapper _mapper;
        public TeacherController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _context.Teachers
                .Include(x => x.Department)
                .ToListAsync();

            var res = _mapper.Map<List<TeacherDTO>>(teachers);

            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teachers = await _context.Teachers
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (teachers == null)
            {
                return NotFound();
            }

            var res = _mapper.Map<TeacherDTO>(teachers);

            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDTO dto)
        {
            var res = _mapper.Map<Teacher>(dto);

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

           var res = _mapper.Map(dto, teacher);

            await _context.SaveChangesAsync();
            return NoContent();
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
