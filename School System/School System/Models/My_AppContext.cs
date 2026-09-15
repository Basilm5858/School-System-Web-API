using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Enrollment>().HasKey(x => new { x.StudentId, x.SubjectId });
            modelBuilder.Entity<Student>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<Student>().Property(x => x.Email).HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(x => x.DateOfBirth).IsRequired();

            //modelBuilder.Entity<Student>().HasData(
            //    new Student { Id = 1, FirstName = "basil", LastName= "Mohamed",  }
            //    );

            base.OnModelCreating(modelBuilder);
        }
    }
}
