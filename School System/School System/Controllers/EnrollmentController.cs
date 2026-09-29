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
        private readonly IEnrollment _customrepo;
        private readonly IMapper _mapper;
        public EnrollmentController(IMapper mapper, IEnrollment customerRepo)
        {
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
            var enrollments = await _customrepo.GetById(id);
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

            await _customrepo.AddAsync(enrollment);
            await _customrepo.SaveChangesAsync();
            return Ok(enrollment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEnrollment(int id, UpdateEnrollmentDTO dto)
        {
            var enrollment = await _customrepo.GetById(id);
            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }

            _mapper.Map(dto, enrollment);
            await _customrepo.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _customrepo.GetById(id);

            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }

            _customrepo.Delete(enrollment);
            await _customrepo.SaveChangesAsync();
            return NoContent();

        }

        [HttpGet("EndPoint6")]
        public async Task<IActionResult> EndPoint6(int subjectid)
        {
            var enrollment = await _customrepo.EndPoint6(subjectid);
            if (enrollment == null)
            {
                return NotFound();
            }
            var res = _mapper.Map<Enrollment>(enrollment);

            return Ok(res);
        }

    }
}
