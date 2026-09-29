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
    public class SubjectController : ControllerBase
    {
        private readonly ISubject _customRepo;
        private readonly IMapper _mapper;

        public SubjectController(IMapper mapper, ISubject customRepo)
        {
            _mapper = mapper;
            _customRepo = customRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubject()
        {
            var subjects = await _customRepo.GetSubjectsWithTeachers();

            var res = _mapper.Map<List<SubjectDTO>>(subjects);

            return Ok(res);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _customRepo.GetById(id);

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

            await _customRepo.AddAsync(subject);
            await _customRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = subject.Id }, subject);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDTO dto)
        {
            var subject = await _customRepo.GetById(id);
            if (subject == null)
            {
                return NotFound("Subject Not Found");
            }

            _mapper.Map(dto, subject);
            await _customRepo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _customRepo.GetById(id);
            if (subject == null)
            {
                return NotFound("Student Not Found");
            }

            _customRepo.Delete(subject);
            await _customRepo.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("EndPoint2")]
        public async Task<IActionResult> EndPoint2(int id)
        {
            var subject = await _customRepo.EndPoint2Async(id);
            if(subject == null)
            {
                return NotFound();
            }
            var res = _mapper.Map<List<SubjectDTO>>(subject);
            return Ok(res);
        }

        [HttpGet("EndPoint7")]
        public async Task<IActionResult> EndPoint7(int teacherid)
        {
            var subject = await _customRepo.EndPoint7(teacherid);

            if (subject == null)
            {
                return NotFound();
            }

            var res = _mapper.Map<List<SubjectDTO>>(subject);
            return Ok(res);
        }
        [HttpGet("EndPoint11")]
        public async Task<IActionResult> EndPoint11(int id1, int id2, int id3)
        {
            var subject = await _customRepo.EndPoint11(id1, id2, id3);

            //var res = _mapper.Map<List<SubjectDTO>>(subject);

            return Ok(subject);
        }

        [HttpGet("EndPoint13")]
        public async Task<IActionResult> EndPoint13(int teacherid)
        {
            var subject = await _customRepo.EndPoint13(teacherid);

            return Ok(subject);
        }
        [HttpGet("EndPoint14")]
        public async Task<IActionResult> EndPoint14(int departmentid)
        {
            var subject = await _customRepo.EndPoint14(departmentid);

            return Ok(subject);
        }

        [HttpGet("EndPoint16")]
        public async Task<IActionResult> EndPoint16()
        {
            var subject = await _customRepo.EndPoint16();

            return Ok(subject);
        }
    }
}
