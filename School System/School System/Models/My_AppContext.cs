using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School_System.Models
{
    public class My_AppContext : DbContext
    {
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassRoom> Classrooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasOne(s => s.Department)
                .WithMany(x => x.Teachers)
                .HasForeignKey(x => x.DepartmentId);

            modelBuilder.Entity<Student>()
                .HasOne(x => x.ClassRoom)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ClassRoomId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Subject)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.SubjectId);

            modelBuilder.Entity<Subject>()
                .HasOne(x => x.Teacher)
                .WithMany(x => x.Subjects)
                .HasForeignKey(x => x.TeacherId);

            modelBuilder.Entity<ClassRoom>()
                .HasMany(x => x.Students)
                .WithOne(x => x.ClassRoom)
                .HasForeignKey(x => x.ClassRoomId);


            modelBuilder.Entity<Enrollment>().HasIndex(x => new { x.StudentId, x.SubjectId }).IsUnique();

            modelBuilder.Entity<Student>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<Student>().Property(x => x.Email).HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(x => x.DateOfBirth).IsRequired();


            modelBuilder.Entity<ClassRoom>().HasData(new ClassRoom { Id = 1, Name = "Room 101", GradeLevel = 10, Capacity = 30 });
            modelBuilder.Entity<ClassRoom>().HasData(new ClassRoom { Id = 2, Name = "Room 102", GradeLevel = 11, Capacity = 30 });
            modelBuilder.Entity<ClassRoom>().HasData(new ClassRoom { Id = 3, Name = "Room 103", GradeLevel = 12, Capacity = 30 });

            modelBuilder.Entity<Student>().HasData(new Student { Id = 1, FirstName = "Basil", LastName = "Mohamed", Email = "basil.mohamed@example.com", DateOfBirth = new DateOnly(2000, 1, 1), ClassRoomId = 1, PhoneNumber = "01158589053" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 2, FirstName = "Ahmed", LastName = "Ali", Email = "ahmed.ali@example.com", DateOfBirth = new DateOnly(2001, 5, 15), ClassRoomId = 2, PhoneNumber = "01158589054" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 3, FirstName = "Fatima", LastName = "Mohamed", Email = "fatima.mohamed@example.com", DateOfBirth = new DateOnly(2002, 9, 20), ClassRoomId = 3, PhoneNumber = "01158589055" });

            modelBuilder.Entity<Department>().HasData(new Department { Id = 1, Name = "Mathematics", Description = "Department of Mathematics" });
            modelBuilder.Entity<Department>().HasData(new Department { Id = 2, Name = "Physics", Description = "Department of Physics" });
            modelBuilder.Entity<Department>().HasData(new Department { Id = 3, Name = "Chemistry", Description = "Department of Chemistry" });

            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", DepartmentId = 1, Salary = 50000, PhoneNumber = "01158589056" });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", DepartmentId = 2, Salary = 55000, PhoneNumber = "01158589057" });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", DepartmentId = 3, Salary = 60000, PhoneNumber = "01158589058" });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 4, FirstName = "Alice", LastName = "Williams", Email = "alice.williams@example.com", DepartmentId = 1, Salary = 52000, PhoneNumber = "01158589059" });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 5, FirstName = "Charlie", LastName = "Brown", Email = "charlie.brown@example.com", DepartmentId = 2, Salary = 58000, PhoneNumber = "01158589060" });


            modelBuilder.Entity<Subject>().HasData(new Subject { Id = 1, Name = "Mathematics", Description = "Mathematics subject", TeacherId = 1, MaxGrade = 100 });
            modelBuilder.Entity<Subject>().HasData(new Subject { Id = 2, Name = "Physics", Description = "Physics subject", TeacherId = 2, MaxGrade = 100 });
            modelBuilder.Entity<Subject>().HasData(new Subject { Id = 3, Name = "Chemistry", Description = "Chemistry subject", TeacherId = 3, MaxGrade = 100 });

            modelBuilder.Entity<Enrollment>().HasData(new Enrollment { Id = 1, StudentId = 1, SubjectId = 2, Grade = 90, EnrollmentDate = new DateTime(2023, 1, 15) });
            modelBuilder.Entity<Enrollment>().HasData(new Enrollment { Id = 2, StudentId = 2, SubjectId = 1, Grade = 85, EnrollmentDate = new DateTime(2023, 1, 16) });
            modelBuilder.Entity<Enrollment>().HasData(new Enrollment { Id = 3, StudentId = 3, SubjectId = 2, Grade = 88, EnrollmentDate = new DateTime(2023, 1, 17) });

            base.OnModelCreating(modelBuilder);
        }
    }
}
