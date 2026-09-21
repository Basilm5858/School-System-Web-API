using AutoMapper;
using School_System.DTOs.ClassRoomDTOs;
using School_System.DTOs.DepartmentDTO.DepartmentDTOs;
using School_System.DTOs.DepartmentDTOs;
using School_System.DTOs.TeacherDTOs;
using School_System.Models;

namespace School_System.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // =========================
            // Department
            // =========================

            CreateMap<Department, DepartmentDTO>();

            CreateMap<CreateDepartmentDTO, Department>();

            CreateMap<UpdateDepartmentDTO, Department>();


            CreateMap<Teacher, TeacherDTO>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)
                )
                .ForMember(
                    dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.Name)
                );

            CreateMap<CreateTeacherDTO, Teacher>();

            CreateMap<UpdateTeacherDTO, Teacher>();

            // =========================
            // ClassRoom
            // =========================

            CreateMap<ClassRoom, ClassRoomsDTO>();

            CreateMap<CreateClassRoomsDTO, ClassRoom>();

            CreateMap<UpdateClassRoomsDTO, ClassRoom>();
        }
    }
}
