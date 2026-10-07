using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public DepartmentController(IMapper mapper, IUnitOfWork deptrepo)
        {
            _mapper = mapper;
            _unitofwork = deptrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _unitofwork.DepartmentRepo.GetAll();

            var result = _mapper.Map<List<DepartmentDTO>>(departments);

            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchByTeacherName(string fullName)
        {
            var department = await _unitofwork.DepartmentRepo.SearchByTeacherName(fullName);

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
            var department = await _unitofwork.DepartmentRepo.GetById(id);


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

            await _unitofwork.DepartmentRepo.AddAsync(department);
            await _unitofwork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDTO dto)
        {
            var department = await _unitofwork.DepartmentRepo.GetById(id);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            _mapper.Map(dto, department);
            await _unitofwork.SaveChangesAsync();
                
            return NoContent();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _unitofwork.DepartmentRepo.GetById(id);

            if (res == null)
            {
                return BadRequest("Department Not Found");
            }

            _unitofwork.DepartmentRepo.Delete(res);
            await _unitofwork.SaveChangesAsync();
            return NoContent();
        }
    }
}
