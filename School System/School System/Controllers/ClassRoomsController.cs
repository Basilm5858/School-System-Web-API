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
    public class ClassRoomsController : ControllerBase
    {
        private readonly IClassRoomRepo _repo;
        private readonly IMapper _mapper;
        public ClassRoomsController(IMapper mapper, IClassRoomRepo repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassRooms()
        {
            var classRooms = await _repo.GetAll();

            if(classRooms == null)
            {
                return NotFound();
            }


            var res = _mapper.Map<List<ClassRoomsDTO>>(classRooms);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoomById(int id)
        {
            var classRoom = await _repo.GetById(id);

            if (classRoom == null)
            {
                return NotFound();
            }

            var res = _mapper.Map<ClassRoomsDTO>(classRoom);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassRoom(CreateClassRoomsDTO dto)
        {
            var res = _mapper.Map<ClassRoom>(dto);

            if(res == null)
            {
                return BadRequest("ClassRoom Cannot Be Null");
            }

            await _repo.Add(res);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassRoomById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassRoom(int id, UpdateClassRoomsDTO dto)
        {
            var classRoom = await _repo.GetById(id);

            if (classRoom == null)
            {
                return NotFound("ClassRoom Not Found");
            }

            var res = _mapper.Map(dto, classRoom);

            await _repo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            var classRoom = await _repo.GetById(id);

            if (classRoom == null)
            {
                return NotFound("ClassRoom Not Found");
            }

            _repo.Delete(classRoom);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
