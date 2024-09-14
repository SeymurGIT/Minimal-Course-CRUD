using Microsoft.EntityFrameworkCore;
using MinimalCourseCRUD.Entity;

namespace MinimalCourseCRUD.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>(); 
        public DbSet<CourseRegistration> CourseRegistrations => Set<CourseRegistration>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
    }
}
