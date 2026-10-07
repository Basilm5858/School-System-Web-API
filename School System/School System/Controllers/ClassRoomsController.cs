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
    public class ClassRoomsController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public ClassRoomsController(IMapper mapper, IUnitOfWork repo)
        {
            _unitofwork = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassRooms()
        {
            var classRooms = await _unitofwork.ClassRoomCustomRepo.GetAll();

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
            var classRoom = await _unitofwork.ClassRoomCustomRepo.GetById(id);

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

            await _unitofwork.ClassRoomCustomRepo.AddAsync(res);
            await _unitofwork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassRoomById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassRoom(int id, UpdateClassRoomsDTO dto)
        {
            var classRoom = await _unitofwork.ClassRoomCustomRepo.GetById(id);

            if (classRoom == null)
            {
                return NotFound("ClassRoom Not Found");
            }

            var res = _mapper.Map(dto, classRoom);
            await _unitofwork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            var classRoom = await _unitofwork.ClassRoomCustomRepo.GetById(id);

            if (classRoom == null)
            {
                return NotFound("ClassRoom Not Found");
            }

            _unitofwork.ClassRoomCustomRepo.Delete(classRoom);
            await _unitofwork.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("EndPoint3")]
        public async Task<IActionResult> EndPoint3(int capacity)
        {
            var classRoom = await _unitofwork.ClassRoomCustomRepo.EndPoint3(capacity);

            if(classRoom == null)
            {
                return NotFound();
            }
            var res = _mapper.Map<ClassRoomsDTO>(classRoom);
            return Ok(res);
        }
        [HttpGet("EndPoint5")]
        public async Task<IActionResult> EndPoint5(string name)
        {
            var classes = await _unitofwork.ClassRoomCustomRepo.EndPoint5(name);

            if (classes == null)
            {
                return NotFound();
            }
            var res = _mapper.Map<ClassRoomsDTO>(classes);
            return Ok(res);
        }
        [HttpGet("EndPoint8")]
        public async Task<IActionResult> EndPoint8(int index)
        {
            var classes = await _unitofwork.ClassRoomCustomRepo.EndPoint8(index);

            if (classes == null)
            {
                return NotFound();
            }
            var res = _mapper.Map<ClassRoomsDTO>(classes);
            return Ok(res);
        }
        [HttpGet("EndPoint10")]
        public async Task<IActionResult> EndPoint10(int gradelevel, int capacity)
        {
            var classes = await _unitofwork.ClassRoomCustomRepo.EndPoint10(gradelevel, capacity);

            return Ok(classes);
        }
    }
}
