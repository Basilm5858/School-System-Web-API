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
    public class AllLinqsController : ControllerBase
    {
        private readonly My_AppContext _context;
        private readonly IMapper _mapper;
        public AllLinqsController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }

        // (1)

        [HttpGet("SearchByClassRoomIdUsingWhere")]
        public async Task<IActionResult> Linq1()
        {
            var filter = await _context.Students
                .Include(x => x.ClassRoom)
                .Where(x => x.ClassRoom.Id == 2)
                .ToListAsync();

            if(filter == null)
            {
                return BadRequest("No Students Found");
            }

            var res = _mapper.Map<List<StudentDTO>>(filter);

            return Ok(res);

        }

        // (2)

        [HttpGet("SearchByClassRoomIdUsingFirstAsync")]
        public async Task<IActionResult> Linq2()
        {
            var filter = await _context.Students
                .Include(x => x.ClassRoom)
                .FirstAsync(x => x.ClassRoom.Id == 2);

            var res = _mapper.Map<StudentDTO>(filter);

            return Ok(res);
        }

        // (3)

        [HttpGet("SearchByClassRoomIdUsingFirstOrDefaultAsync")]
        public async Task<IActionResult> Linq3()
        {
            var filter = await _context.Students
                .Include(x => x.ClassRoom)
                .FirstOrDefaultAsync(x => x.ClassRoom.Id == 99);

            var res = _mapper.Map<StudentDTO>(filter);

            return Ok(res);
        }

        // (4)

        [HttpGet("SearchByStudentEmailUsingSingleAsync")]
        public async Task<IActionResult> Linq4()
        {
            var filter = await _context.Students
                .SingleAsync(x => x.Email == "basil@gmail.com");

            var res = _mapper.Map<StudentDTO>(filter);

            return Ok(res);
        }

        // (5)

        [HttpGet("SearchByStudentEmailUsingSingleOrDefaultAsync")]
        public async Task<IActionResult> Filters()
        {
            var filter = await _context.Students
                .SingleOrDefaultAsync(x => x.Email == "basil@gmail.com");

            var res = _mapper.Map<StudentDTO>(filter);

            return Ok(res);
        }

        // (6)

        [HttpGet("GetLastEnrollmentUsingLastAsync")]
        public async Task<IActionResult> Linq6()
        {
            var filter = await _context.Enrollments
                .LastAsync(x => x.StudentId == 10);

            var res = _mapper.Map<EnrollmentDTO>(filter);

            return Ok(res);
        }

        // (7)

        [HttpGet("GetLastEnrollmentUsingLastOrDefaultAsync")]
        public async Task<IActionResult> Linq7()
        {
            var filter = await _context.Enrollments
                .LastOrDefaultAsync(x => x.StudentId == 10);

            var res = _mapper.Map<EnrollmentDTO>(filter);

            return Ok(res);
        }


        // (8)

        [HttpGet("GetStudentByIndexUsingElementAtAsync")]
        public async Task<IActionResult> Linq8(int index)
        {
            var filter = await _context.Students
                .ElementAtAsync(index);

            var res = _mapper.Map<StudentDTO>(filter);

            return Ok(res);
        }

        // (15)

        [HttpGet("SortByLastName")]
        public async Task<IActionResult> Linq15()
        {
            var res = await _context.Students
                 .OrderBy(x => x.LastName)
                 .ToListAsync();

            return Ok(res);
        }


        [HttpGet("GetAvgGrade")]
        public async Task<IActionResult> GetAVG(int id)
        {
            var res = await _context.Enrollments
                .Where(x => x.Student.ClassRoomId == id)
                .Select(x => new
                {
                    x.Grade
                }).AverageAsync(x => x.Grade);

            return Ok(res);
        }



        [HttpGet("GroupBy")]
        public async Task<IActionResult> GroupByClassroom()
        {
            var res = await _context.Students
                .GroupBy(x => x.ClassRoomId)
                .Select(x => new
                {
                    ClassRoomId = x.Key,
                    Count = x.Count(),
                    StudentId = x.Select(g => new {
                        g.Id,
                        g.FirstName
                    })
                }).ToListAsync();

            return Ok(res);
        }

        
        // (16)

        [HttpGet("SortByGrade")]
        public async Task<IActionResult> SortByLastName(int classid)
        {
            var res = await _context.Enrollments
                .Where(x => x.Student.ClassRoomId == classid)
                 .OrderByDescending(x => x.Grade)
                 .Select(x => new EnrollmentDTO
                 {
                     Id = x.Id,
                     StudentFullName = x.Student.FirstName + " " + x.Student.LastName,
                     SubjectName = x.Subject.Name,
                     StudentId = x.StudentId,
                     Grade = x.Grade
                 }).ToListAsync();

            return Ok(res);
        }
    }
}
