using School_System.Repo.Implementations;

namespace School_System.Repo.Interfaces
{
    public interface IUnitOfWork
    {
        EnrollmentCustomRepo EnrollmentCustomRepo { get; }
        AuthCustomRepo AuthCustomRepo { get; }
        ClassRoomCustomRepo ClassRoomCustomRepo { get; }
        DepartmentRepo DepartmentRepo { get; }
        StuedentCustomRepo StuedentCustomRepo { get; }
        TeacherCustomRepo TeacherCustomRepo { get; }
        SubjectCustomRepo SubjectCustomRepo { get; }

        Task SaveChangesAsync();
    }
}
