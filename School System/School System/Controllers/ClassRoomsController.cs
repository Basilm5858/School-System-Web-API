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
        public ClassRoomsController()
        {
            _context = new My_AppContext();
        }
        [HttpGet]
        public async Task<IActionResult> GetClassRooms()
        {
            var classRooms = await _context.Classrooms
                .Select(x => new ClassRoomsDTO
                {
                    Name = x.Name,
                    Capacity = x.Capacity,
                    GradeLevel = x.GradeLevel,
                    Id = x.Id,
                }).ToListAsync();

            return Ok(classRooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoomById(int id)
        {
            var classRoom = await _context.Classrooms
                .Select(x => new ClassRoomsDTO
                {
                    Name = x.Name,
                    Capacity = x.Capacity,
                    GradeLevel = x.GradeLevel,
                    Id = x.Id,
                }).FirstOrDefaultAsync(x => x.Id == id);

            if (classRoom == null)
            {
                return NotFound();
            }

            return Ok(classRoom);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassRoom(CreateClassRoomsDTO dto)
        {
            var res = new ClassRoom
            {
                Name = dto.Name,
                Capacity = dto.Capacity,
                GradeLevel = dto.GradeLevel
            };

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
            classRoom.Name = dto.Name;
            classRoom.Capacity = dto.Capacity;
            classRoom.GradeLevel = dto.GradeLevel;

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
