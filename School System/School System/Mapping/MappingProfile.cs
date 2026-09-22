using AutoMapper;
using School_System.DTOs.ClassRoomDTOs;
using School_System.DTOs.DepartmentDTO.DepartmentDTOs;
using School_System.DTOs.DepartmentDTOs;
using School_System.DTOs.EnrollmentDTOs;
using School_System.DTOs.StudentDTOs;
using School_System.DTOs.SubjectDTOs;
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

            // =========================
            // Teacher
            // =========================

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

            // =========================
            // Student
            // =========================

            CreateMap<Student, StudentDTO>()
                .ForMember(
                x => x.FullName,
                opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)
                ).ForMember(
                x => x.ClassRoomName,
                opt => opt.MapFrom(src => src.ClassRoom.Name)
                );

            CreateMap<CreateStudentDTO, Student>();

            CreateMap<UpdateStudentDTO, Student>();

            // =========================
            // Subject
            // =========================
            CreateMap<Subject, SubjectDTO>()
                .ForMember(
                x => x.TeacherName,
                opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName)
                );

            CreateMap<CreateSubjectDTO, Subject>();
            CreateMap<UpdateSubjectDTO, Subject>();

            // =========================
            // Enrollment
            // =========================

            CreateMap<Enrollment, EnrollmentDTO>()
                .ForMember(
                x => x.StudentFullName,
                opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName)
                ).ForMember(
                x => x.SubjectName,
                opt => opt.MapFrom(src => src.Subject.Name)
                );

            CreateMap<CreateEnrollmentDTO, Enrollment>();
            CreateMap<UpdateEnrollmentDTO, Enrollment>();

        }
    }
}
