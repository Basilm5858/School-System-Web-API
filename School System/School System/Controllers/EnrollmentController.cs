using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.EnrollmentDTOs;
using School_System.DTOs.StudentDTOs;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly My_AppContext _context;
        private readonly IMapper _mapper;
        public EnrollmentController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }

        // (6)

        //[HttpGet("Linq")]
        //public async Task<IActionResult> Filters()
        //{
        //    var filter = await _context.Enrollments
        //        .LastAsync(x => x.StudentId == 10);

        //    var res = _mapper.Map<EnrollmentDTO>(filter);

        //    return Ok(res);
        //}

        // (7)

        //[HttpGet("Linq")]
        //public async Task<IActionResult> Filters()
        //{
        //    var filter = await _context.Enrollments
        //        .LastOrDefaultAsync(x => x.StudentId == 10);

        //    var res = _mapper.Map<EnrollmentDTO>(filter);

        //    return Ok(res);
        //}

        [HttpGet]
        public async Task<IActionResult> GetEnrollments()
        {
            var enrollments = await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .ToListAsync();
            var res = _mapper.Map<List<EnrollmentDTO>>(enrollments);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollments = await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == id);
            var res = _mapper.Map<List<EnrollmentDTO>>(enrollments);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentDTO dto)
        {
            var enrollment = _mapper.Map<Enrollment>(dto);

            if (enrollment == null)
            {
                return BadRequest("Enrollment Cannot Be Null");
            }

            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
            return Ok(enrollment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEnrollment(int id, UpdateEnrollmentDTO dto)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }

            _mapper.Map(dto, enrollment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
