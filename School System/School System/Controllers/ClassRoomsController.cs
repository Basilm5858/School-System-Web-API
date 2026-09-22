using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System.DTOs.ClassRoomDTOs;
using School_System.Models;

namespace School_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomsController : ControllerBase
    {
        private readonly My_AppContext _context;
        private readonly IMapper _mapper;
        public ClassRoomsController(IMapper mapper)
        {
            _context = new My_AppContext();
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetClassRooms()
        {
            var classRooms = await _context.Classrooms
                .ToListAsync();

            var res = _mapper.Map<List<ClassRoomsDTO>>(classRooms);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoomById(int id)
        {
            var classRoom = await _context.Classrooms
               .FirstOrDefaultAsync(x => x.Id == id);

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

            await _context.Classrooms.AddAsync(res);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassRoomById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassRoom(int id, UpdateClassRoomsDTO dto)
        {
            var classRoom = await _context.Classrooms.FindAsync(id);

            if (classRoom == null)
            {
                return NotFound("ClassRoom Not Found");
            }

            var res = _mapper.Map(dto, classRoom);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            var classRoom = await _context.Classrooms.FindAsync(id);

            if (classRoom == null)
            {
                return NotFound("ClassRoom Not Found");
            }

            _context.Classrooms.Remove(classRoom);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
