using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class TeacherController : ControllerBase
    {
        private readonly TeacherCustomRepo _customRepo;
        private readonly IMapper _mapper;
        public TeacherController(IMapper mapper, TeacherCustomRepo customRepo)
        {
            _mapper = mapper;
            _customRepo = customRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _customRepo.GetTeachersWithDepartment();

            var res = _mapper.Map<List<TeacherDTO>>(teachers);

            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teachers = await _customRepo.GetById(id);

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

            await _customRepo.AddAsync(res);
            await _customRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDTO dto)
        {
            var teacher = await _customRepo.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }

           var res = _mapper.Map(dto, teacher);

            await _customRepo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _customRepo.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _customRepo.Delete(teacher);
            await _customRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
