using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs;
using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IGenericRepo<Teacher> _repo;
        private readonly IMapper _mapper;
        public TeacherController(IMapper mapper, IGenericRepo<Teacher> repo)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _repo.GetAll();

            var res = _mapper.Map<List<TeacherDTO>>(teachers);

            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teachers = await _repo.GetById(id);

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

            await _repo.AddAsync(res);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDTO dto)
        {
            var teacher = await _repo.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }

           var res = _mapper.Map(dto, teacher);

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _repo.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _repo.Delete(teacher);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
