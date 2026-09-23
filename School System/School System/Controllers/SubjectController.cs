using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.StudentDTOs;
using School_System.DTOs.SubjectDTOs;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly My_AppContext _context;
        private readonly IMapper _mapper;

        public SubjectController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubject()
        {
            var subjects = await _context.Subjects
                .Include(x => x.Teacher)
                .ToListAsync();

            var res = _mapper.Map<List<SubjectDTO>>(subjects);

            return Ok(res);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _context.Subjects
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (subject == null)
            {
                return BadRequest("Student Not Found");
            }

            var res = _mapper.Map<SubjectDTO>(subject);

            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateSubjectDTO dto)
        {
            var subject = _mapper.Map<Subject>(dto);

            if (subject == null)
            {
                return BadRequest("ClassRoom Cannot Be Null");
            }

            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = subject.Id }, subject);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDTO dto)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound("Subject Not Found");
            }

            _mapper.Map(dto, subject);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound("Student Not Found");
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
