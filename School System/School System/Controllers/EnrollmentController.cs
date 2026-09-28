using System.Diagnostics;
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
    public class EnrollmentController : ControllerBase
    {
        private readonly IGenericRepo<Enrollment> _repo;
        private readonly EnrollmentCustomRepo _customrepo;
        private readonly IMapper _mapper;
        public EnrollmentController(IMapper mapper, IGenericRepo<Enrollment> repo, EnrollmentCustomRepo customerRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _customrepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrollments()
        {
            var enrollments = await _customrepo.GetEnrollmentsWithStudentAndSubject();
            if(enrollments == null)
            {
                return BadRequest("The Enrollment is null");
            }

            var res = _mapper.Map<List<EnrollmentDTO>>(enrollments);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollments = await _repo.GetById(id);
            var res = _mapper.Map<EnrollmentDTO>(enrollments);
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

            await _repo.AddAsync(enrollment);
            await _repo.SaveChangesAsync();
            return Ok(enrollment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEnrollment(int id, UpdateEnrollmentDTO dto)
        {
            var enrollment = await _repo.GetById(id);
            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }

            _mapper.Map(dto, enrollment);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _repo.GetById(id);

            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }

            _repo.Delete(enrollment);
            await _repo.SaveChangesAsync();
            return NoContent();

        }

    }
}
