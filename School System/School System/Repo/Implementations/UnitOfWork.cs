using School_System.Models;
using School_System.Repo.Interfaces;

namespace School_System.Repo.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public EnrollmentCustomRepo EnrollmentCustomRepo { get; }

        public AuthCustomRepo AuthCustomRepo { get; }

        public ClassRoomCustomRepo ClassRoomCustomRepo { get; }

        public DepartmentRepo DepartmentRepo { get; }

        public StuedentCustomRepo StuedentCustomRepo { get; }

        public TeacherCustomRepo TeacherCustomRepo { get; }

        public SubjectCustomRepo SubjectCustomRepo { get; }
        private readonly My_AppContext _context;

        public UnitOfWork(EnrollmentCustomRepo enrollmentCustomRepo, StuedentCustomRepo stuedentCustomRepo,
            TeacherCustomRepo teacherCustomRepo, SubjectCustomRepo subjectCustomRepo,
            ClassRoomCustomRepo classRoomCustomRepo, My_AppContext context, AuthCustomRepo authCustomRepo,
            DepartmentRepo departmentRepo)
        {
            _context = context;
            StuedentCustomRepo = stuedentCustomRepo;
            TeacherCustomRepo = teacherCustomRepo;
            SubjectCustomRepo = subjectCustomRepo;
            ClassRoomCustomRepo = classRoomCustomRepo;
            EnrollmentCustomRepo = enrollmentCustomRepo;
            DepartmentRepo = departmentRepo;
            AuthCustomRepo = authCustomRepo;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
