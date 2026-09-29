using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs;
using School_System.Mapping;
using School_System.Models;
using School_System.Repo.Implementations;
using School_System.Repo.Interfaces;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepo _repo;
        private readonly IMapper _mapper;
        public DepartmentController(IMapper mapper, DepartmentRepo deptrepo)
        {
            _mapper = mapper;
            _repo = deptrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _repo.GetAll();

            var result = _mapper.Map<List<DepartmentDTO>>(departments);

            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchByTeacherName(string fullName)
        {
            var department = await _repo.SearchByTeacherName(fullName);

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
            var department = await _repo.GetById(id);


            if (department == null)
            {
                return NotFound("Department Not Found");
            }

            var res = _mapper.Map<DepartmentDTO>(department);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO dto)
        {
            var department = _mapper.Map<Department>(dto);

            await _repo.AddAsync(department);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDTO dto)
        {
            var department = await _repo.GetById(id);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            _mapper.Map(dto, department);
            await _repo.SaveChangesAsync();
                
            return NoContent();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _repo.GetById(id);

            if (res == null)
            {
                return BadRequest("Department Not Found");
            }

            _repo.Delete(res);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
