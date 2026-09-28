using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs;
using School_System.Models;
using School_System.Repo.Implementations;
using School_System.Repo.Interfaces;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IGenericRepo<Student> _repo;
        private readonly StuedentCustomRepo _customRepo;
        private readonly IMapper _mapper;

        public StudentController(IMapper mapper,IGenericRepo<Student> genericRepo,StuedentCustomRepo customRepo)
        {
            _mapper = mapper;
            _customRepo = customRepo;
            _repo = genericRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var stud = await _customRepo.IncludeClassRoom();

            var res = _mapper.Map<List<StudentDTO>>(stud);

            return Ok(res);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stud = await _repo.GetById(id);

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

            await _repo.AddAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDTO dto)
        {
            var student = await _repo.GetById(id);

            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            _mapper.Map(dto, student);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _repo.GetById(id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            _repo.Delete(student);
            return NoContent();
        }
    }
}
